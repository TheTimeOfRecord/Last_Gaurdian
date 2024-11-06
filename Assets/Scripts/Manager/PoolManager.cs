using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingleTonBase<PoolManager>
{
    [System.Serializable]
    public class Pool
    {
        public string poolName;            // 풀 이름
        public GameObject prefab;          // 풀링할 프리팹
        public int initialSize;            // 초기 생성 개수
        public Transform parentTransform;  // 부모 오브젝트 (옵션)
    }

    public List<Pool> pools; // Inspector에서 설정 가능한 풀 목록

    private Dictionary<string, ObjectPool<GameObject>> poolDictionary;

    protected override void Awake()
    {
        base.Awake();

        InitializePools();
    }

    private void InitializePools()
    {
        poolDictionary = new Dictionary<string, ObjectPool<GameObject>>();

        // 각 풀 정보를 사용해 오브젝트 풀을 초기화합니다.
        foreach (var pool in pools)
        {
            if (pool.prefab != null)
            {
                var newPool = new ObjectPool<GameObject>(pool.prefab, pool.initialSize, pool.parentTransform);
                poolDictionary.Add(pool.poolName, newPool);
            }
            else
            {
                Debug.LogWarning($"Pool {pool.poolName} does not have a valid prefab.");
            }
        }
    }

    // 오브젝트 가져오기
    public GameObject GetFromPool(string poolName, Vector3 position, Quaternion rotation = default)
    {
        if (poolDictionary.ContainsKey(poolName))
        {
            return poolDictionary[poolName].Get(position, rotation);
        }
        Debug.LogWarning($"Pool {poolName} does not exist!");
        return null;
    }

    // 오브젝트 반환
    public void ReturnToPool(string poolName, GameObject obj)
    {
        if (poolDictionary.ContainsKey(poolName))
        {
            poolDictionary[poolName].ReturnToPool(obj);
        }
        else
        {
            Debug.LogWarning($"Pool {poolName} does not exist!");
        }
    }
}
