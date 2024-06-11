using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyPool;
    [SerializeField] private int _spawnInterval;

    private void Start()
    {
        _enemyPool = ObjectPoolManager.Instance.PoolDictionary["Enemy"];
        InitializePosition();
        InvokeRepeating("SpawnObject", 0f, _spawnInterval);
    }

    void SpawnObject()
    {
        foreach (var obj in _enemyPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = GetRandomPosition();
                obj.SetActive(true);
                return;
            }
        }
    }

    void InitializePosition()
    {
        foreach (var _enemy in _enemyPool)
        {
            _enemy.transform.position = GetRandomPosition();
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(0f, 80f);
        float z = Random.Range(40f, 90f);
        Vector3 randomPosition = new Vector3(x, 5f, z);

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 30f, NavMesh.GetAreaFromName("Walkable")))
        {
            randomPosition = hit.position;
            RaycastHit raycastHit;
            if (Physics.Raycast(randomPosition + Vector3.up * 10f, Vector3.down, out raycastHit, 20f))
            {
                randomPosition = raycastHit.point;
            }
        }

        return randomPosition;
    }
}