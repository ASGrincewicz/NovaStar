using System;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(menuName =("Specialized Events/ Pool GameObject Request"))]
    public class PoolGORequest : ScriptableObject
    {
        public Func<GameObject> OnGameObjectRequested;

        public GameObject RequestGameObject()
        {
            if (OnGameObjectRequested != null)
            {
                GameObject obj = OnGameObjectRequested.Invoke();
                return obj;
            }
            else
                return null;
        }
    }
}
