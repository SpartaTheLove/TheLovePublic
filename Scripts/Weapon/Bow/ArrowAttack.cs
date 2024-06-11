// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class ArrowAttack : MonoBehaviour
// {
//     [SerializeField] private GameObject hitParticle;
//     [SerializeField] private float arrowSpeed;
//     [SerializeField] private float arrowMaxDistance;
//     [SerializeField] private int damage;
//
//     private Vector3 startPos;
//     private Rigidbody rb;
//
//     private void Awake()
//     {
//         startPos = transform.position;
//         rb = GetComponent<Rigidbody>();
//     }
//
//     private void Start()
//     {
//         Vector3 dirToTarget = (BowInputSystem.Instance.GetAimTarget() - transform.position).normalized;
//         rb.velocity = dirToTarget * arrowSpeed;
//     }
//
//     private void Update()
//     {
//         CheckDistance();
//     }
//
//     void CheckDistance()
//     {
//         if (Vector3.Distance(transform.position, startPos) >= arrowMaxDistance)
//         {
//             Destroy(gameObject);
//         }
//     }
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Monster"))
//         {
//             // Add damage to the monster
//             Monster monster = other.GetComponent<Monster>();
//             if (monster != null)
//             {
//                 monster.TakeDamage(damage);
//             }
//
//             // Instantiate hit particle effect
//             GameObject obj = Instantiate(hitParticle, other.transform.position, Quaternion.identity);
//             Destroy(obj, 1.0f);
//
//             // Destroy the arrow
//             Destroy(gameObject);
//         }
//     }
// }