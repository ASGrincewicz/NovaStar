using System;
using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///@info: This class acts as an event channel for requests to the Pool Manager.
    ///</summary>
    [CreateAssetMenu(menuName =("Specialized Events/ Pool GameObject Request"))]
    public class PoolGORequest : ScriptableObject
    {
        public Func<int, GameObject> OnGameObjectIntRequested;
       
        public GameObject RequestGameObjectInt(int parameter)
        {
            if (OnGameObjectIntRequested != null)
            {
                GameObject obj = OnGameObjectIntRequested.Invoke(parameter);
                return obj;
            }
            else
                return null;
        }
    }
}
