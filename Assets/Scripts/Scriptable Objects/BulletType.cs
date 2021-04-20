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
    [CreateAssetMenu(fileName = "newBulletType.asset", menuName = "Scriptable Objects/ Bullet Type")]
    public class BulletType : ScriptableObject
    {
        public string bulletName;
        public float speed;
        public bool isPlayerBullet;
        public int damage;
    }
}
