using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newEnemyClass.asset", menuName = "Scriptable Objects/ EnemyClass")]
    public class EnemyClassOld : ScriptableObject
    {
        public string enemyName;
        public int scoreTier;
        public int hp;
        public float speed;
        public float fireRate;
        public GameObject itemDrop;
        public bool hasWeapon;
        public GameObject weapon;
    }
}
