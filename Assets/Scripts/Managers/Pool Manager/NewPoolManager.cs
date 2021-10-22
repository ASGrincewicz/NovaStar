using System;
using System.Collections.Generic;
using UnityEngine;
namespace Grincewicz.PoolManager
{
    public class NewPoolManager : MonoBehaviour
    {
        [SerializeField] private List<Pool> _pools = new List<Pool>();
        public List<Pool> Pools { get => _pools; set => _pools = value; }

        [Header("Listening to:")]
        [SerializeField] private PoolGORequest _pooledObjectRequest;
        
       
        public static Action clearChildren;

        private void OnEnable() => _pooledObjectRequest.OnGameObjectIntRequested += RequestObject;

        private void OnDisable() => _pooledObjectRequest.OnGameObjectIntRequested -= RequestObject;

        private void Start()
        {
            for(int i = 0; i < _pools.Count; i++)
            {
                GenerateObjects(i, _pools[i].initialPoolSize);
            }
        }

        public void GenerateObjects(int poolIndex, int amount)
        {
            var pool = _pools[poolIndex].objectPool;
            var prefab = _pools[poolIndex].poolableObject;
            var container = _pools[poolIndex].objectContainer;

            for (int i = 0; i < amount; i++)
            {
                GameObject obj = Instantiate(prefab, container);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
       
        public Pool CreateNewPool(int poolIndex, GameObject pooledObject, int intialAmount)
        {
            var container = new GameObject($"{pooledObject.name}_container").transform;
            Instantiate(container, transform);
            var newPool = new Pool(pooledObject, container, intialAmount);
            if (poolIndex !> _pools.Count - 1)
                _pools[poolIndex] = newPool;
            else
                _pools.Add(newPool);
            return newPool;
        }
        public void DestroyPool(int poolIndex)
        {
            for(int i = 0; i < _pools[poolIndex].objectPool.Count; i++)
            {
                Destroy(_pools[poolIndex].objectPool[i]);
                Destroy(_pools[poolIndex].objectContainer);
                _pools.Remove(_pools[poolIndex]);
            }
        }

        private GameObject RequestObject(int poolIndex)
        {
            var pool = _pools[poolIndex].objectPool;
            var prefab = _pools[poolIndex].poolableObject;
            var container = _pools[poolIndex].objectContainer;

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    pool[i].SetActive(true);
                    return pool[i];
                }
            }
            GameObject newObject = Instantiate(prefab, container);
            newObject.SetActive(true);
            pool.Add(newObject);
            return newObject;
        }
    }
}
