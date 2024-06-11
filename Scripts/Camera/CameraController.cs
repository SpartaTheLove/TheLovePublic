using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerInputController _playerInputController;

    public Transform player;
    public Vector3 offset;
    public float sensitivity = 0.5f;

    private float _currentX = 0f;
    private float _currentY = 0f;
    private float _minY = 10f;
    private float _maxY = 60f;

    public float defaultDistance = 5f;

    public float zoomSpped = 5f;
    public float originalFieldofView = 70f;
    public float zoomFieldofView = 20f;
    public string FireInput = "Fire2";
    public string AimingInput = "Fire2";
    public Camera mainCam;

    private float defaultMinY = 10f;
    public float zoomedMinY = -20f;

    private void Start()
    {
        _playerInputController = player.GetComponent<PlayerInputController>();
        if (_playerInputController != null)
        {
            _playerInputController.OnLookEvent += Look;
        }

        mainCam = Camera.main;

        _currentX = player.eulerAngles.y;
    }

    private void Look(Vector2 lookVector)
    {
        _currentX += lookVector.x * sensitivity;
        _currentY -= lookVector.y * sensitivity;

        _currentY = Mathf.Clamp(_currentY, _minY, _maxY);
    }

    private void LateUpdate()
    {
        ZoomCamera();

        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);

        float distance = Mathf.Clamp(offset.magnitude, defaultDistance, defaultDistance);
        Vector3 direction = rotation * new Vector3(0, 0, -distance);
        Vector3 position = player.position + direction;

        transform.position = position;
        transform.LookAt(player.position);
    }

    void ZoomCamera()
    {
        if (Input.GetButton(AimingInput))
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, zoomFieldofView, zoomSpped * Time.deltaTime);
            _minY = zoomedMinY;
        }
        else
        {
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, originalFieldofView, zoomSpped * Time.deltaTime);
            _minY = defaultMinY;
        }
    }
}
