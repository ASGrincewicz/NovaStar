using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Veganimus.GDHQcert
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    [CreateAssetMenu(fileName = "newEnemyWave.asset", menuName = "Scriptable Objects/ Player Weapon")]
    public class PlayerWeapon : ScriptableObject
    { 
        public string weaponName;
        public int weaponID;
        public int damage;
        public GameObject projectile;
        public float fireRate;
    }
}
