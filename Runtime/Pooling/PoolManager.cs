//-----------------------------------------------------------------------
// <copyright file="PoolManager.cs" company="Lost Signal LLC">
//     Copyright (c) Lost Signal LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

#if UNITY

namespace Lost
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolManager : SingletonMonoBehaviour<PoolManager>, IName
    {
        private static readonly Vector3 DefaultPosition = Vector3.zero;
        private static readonly Quaternion DefaultRotation = Quaternion.identity;

        private Dictionary<int, Pool> pools = new Dictionary<int, Pool>();

        public string Name => "Pool Manager";

        public static GameObject InstantiatePrefab(GameObject gameObjectPrefab, Transform parent = null)
        {
            return InstantiatePrefab(gameObjectPrefab, DefaultPosition, DefaultRotation, parent);
        }

        public static T InstantiatePrefab<T>(T prefab, Transform parent = null)
            where T : Component
        {
            var gameObject = InstantiatePrefab(prefab.gameObject, DefaultPosition, DefaultRotation, parent);
            return gameObject.GetComponent<T>();
        }

        public static GameObject InstantiatePrefab(GameObject gameObjectPrefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            int poolId = gameObjectPrefab.GetInstanceID();

            if (Instance.pools.TryGetValue(poolId, out Pool pool))
            {
                return pool.GetPooledObject(position, rotation, parent);
            }
            else
            {
                Debug.LogError("Tried to get pooled prefab that isn't pooled.  Creating on the fly, please add a Pool for this object.", gameObjectPrefab);

                var gameObject = GameObject.Instantiate(gameObjectPrefab);

                if (parent != null)
                {
                    gameObject.transform.SetParent(parent);
                }

                gameObject.transform.SetPositionAndRotation(position, rotation);

                return gameObject;
            }
        }

        public static void DestroyGameObject(GameObject gameObject)
        {
#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                GameObject.Destroy(gameObject);
                return;
            }
#endif

            Instance.InternalDestory(gameObject, false);
        }

        public static void DestroyGameObjectImmediate(GameObject gameObject)
        {
#if UNITY_EDITOR
            if (Application.isPlaying == false)
            {
                GameObject.DestroyImmediate(gameObject);
                return;
            }
#endif

            Instance.InternalDestory(gameObject, true);
        }

        internal void RegisterPool(Pool pool)
        {
            this.pools.Add(pool.PoolId, pool);
        }

        internal void UnregisterPool(Pool pool)
        {
            this.pools.Remove(pool.PoolId);
        }

        private void InternalDestory(GameObject gameObject, bool destroyImmediate)
        {
            if (gameObject.TryGetComponent<PooledObject>(out PooledObject pooledObject))
            {
                pooledObject.ReturnToPool();
            }
            else
            {
                if (destroyImmediate)
                {
                    GameObject.DestroyImmediate(gameObject);
                }
                else
                {
                    GameObject.Destroy(gameObject);
                }
            }
        }
    }
}

#endif
