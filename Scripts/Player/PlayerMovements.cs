using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    PlayerInputController _playerInputController;
    PlayerAnimController _playerAnimController;
    private Rigidbody _playerRigidbody;
    private Vector3 _movementDirection;

    private BoxCollider _playerCollider;
    private Vector3 _initialCenter;
    public GameObject bodyObject;

    private CharacterState _characterState = CharacterState.Idle;

    //3rd Camera Info
    public GameObject cameraContainer;
    public Transform player;
    public float rotationSpeed = 10f;

    [SerializeField]
    public float curSpeed = 3f;
    public float moveSpeed;
    public float runSpeed;
    public float jumpPower;

    private void Awake()
    {
        _playerInputController = GetComponent<PlayerInputController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimController= GetComponent<PlayerAnimController>();
        _playerCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        _playerInputController.OnMoveEvent += Move;
        _playerInputController.OnJumpEvent += Jump;
        _playerInputController.OnRunEvent += Run;
        _playerInputController.OnAttackEvent += Attack;

        _initialCenter = _playerCollider.center;
    }

    private void FixedUpdate()
    {

        if (_movementDirection == Vector3.zero)
        {
            _characterState = CharacterState.Idle;
        }
        else if (_movementDirection != null)
        {
            _characterState = _playerInputController.isRun ? CharacterState.Run : CharacterState.Walk;     
        }
        //var state2 = cameraContainer.transform.rotation;

        //this.transform.eulerAngles += new Vector3(0, state2.y, 0);

        ApplyMovement(_movementDirection, curSpeed);
        _playerAnimController.SetAnimator(_characterState);
        bodyObject.transform.localPosition = Vector3.zero; //set body position init

        RotatePlayerToMousePosition();
    }

    public void Move(Vector2 direction)
    {
        Vector3 cameraForward = cameraContainer.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();
        Vector3 cameraRight = cameraContainer.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        _movementDirection = (cameraForward * direction.y + cameraRight * direction.x).normalized;
        _characterState = _playerInputController.isRun ? CharacterState.Run : CharacterState.Walk;
    }

    private void ApplyMovement(Vector3 direction, float speed)
    {
        direction = direction.normalized * speed;
        Vector3 currentVelocity = _playerRigidbody.velocity; //set Default gravity
        _playerRigidbody.velocity = new Vector3(direction.x, currentVelocity.y, direction.z);

        transform.LookAt(transform.position + _movementDirection);

        _playerCollider.center = _initialCenter;
    }
    public void Jump()
    {
        _playerRigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        _playerAnimController.IsJump();
    }

    public void Run()
    {
        curSpeed = _playerInputController.isRun ? runSpeed : moveSpeed;
    }

    public void Attack()
    {
        if (_playerInputController.isAttacking)
        {
            
        }
    }

    private void RotatePlayerToMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Plane playerPlane = new Plane(Vector3.up, player.localPosition);
        float hitDist;

        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Vector3 directionToLook = targetPoint - player.localPosition;
            directionToLook.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToLook);
            Quaternion smoothedRotation = Quaternion.Slerp(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            player.rotation = Quaternion.Euler(0, smoothedRotation.eulerAngles.y, 0);
        }
    }
}
