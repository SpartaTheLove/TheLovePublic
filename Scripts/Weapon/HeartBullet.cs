using System;
using UnityEngine;

public class HeartBullet : MonoBehaviour
{
    [SerializeField] private GameObject _hitParticle;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _bulletDistance;

    private Vector3 _startPos;
    private Rigidbody _rigidbody;

    [SerializeField] private int _damage;

    private void Awake()
    {
        _startPos = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 _dirToPlayer = ((CharacterManager.Instance.Player.transform.position + Vector3.up) - transform.position).normalized;
        _rigidbody.velocity = _dirToPlayer * _bulletSpeed;
    }

    private void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(transform.position, _startPos) >= _bulletDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            CharacterManager.Instance.Player.condition.TakePhysicalDamage(_damage);
            GameObject _obj = Instantiate(_hitParticle, other.transform.position + Vector3.up, Quaternion.identity);
            Destroy(_obj, 1.0f); 
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}