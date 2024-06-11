using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bx;
    public float destroyTime = 10f;

    [SerializeField] private GameObject hitParticle;

    TrailRenderer arrowTrail;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<BoxCollider>();
        arrowTrail = GetComponent<TrailRenderer>();

        rb.isKinematic = true;
        bx.isTrigger = true;
    }

    private void Start()
    {
        arrowTrail.enabled = true;
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        if (!rb.isKinematic)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6)
        {
            if (hitParticle != null)
            {
                GameObject obj = Instantiate(hitParticle, other.transform.position, Quaternion.identity);
                Destroy(obj, 1.0f);
            }

            Destroy(gameObject);
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakePhysicalDamage(5);
            Destroy(gameObject);
        }
    }

    public void Fire(Vector3 direction, float force)
    {
        rb.isKinematic = false;
        rb.velocity = direction * force;
    }
}
