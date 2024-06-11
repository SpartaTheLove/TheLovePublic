using System.Collections.Generic;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _resourcePool;
    [SerializeField] LayerMask groundMask;
    [SerializeField] private int _spawnInterval;

    private void Start()
    {
        _resourcePool = ObjectPoolManager.Instance.PoolDictionary["Resource"];
        InitializePosition();
        InvokeRepeating("SpawnObject", 0f, _spawnInterval);
    }

    void SpawnObject()
    {
        foreach (var obj in _resourcePool)
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
        foreach (var _resource in _resourcePool)
        {
            _resource.transform.position = GetRandomPosition();
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(0f, 90f);
        float z = Random.Range(35f, 100f);
        Vector3 randomPosition = new Vector3(x, 5f, z); 

        RaycastHit hit;

        if (Physics.Raycast(randomPosition, Vector3.down, out hit, groundMask))
        {
            randomPosition = hit.point + new Vector3(0, 0.3f, 0);
        }

        return randomPosition;
    }
}