//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace Veganimus{
//    ///<summary>
//    ///@author
//    ///Aaron Grincewicz
//    ///</summary>
//    public class PlayerProjectile :Projectile
//    {
       
//        public float xVelocity;
//        public GameObject projPrefab;
//        public Transform firePos;
       
//        private void OnEnable()
//        {
//            //Movement listens for weapon fire event
//            _inputReader.shootEvent += OnShoot;
//        }

//        private void Start()
//        {
//            _rigidbody = GetComponent<Rigidbody>();
            
//        }
//        private void FixedUpdate()
//        {
//            xVelocity = _rigidbody.velocity.x;
//        }

//        //attached to weapon
//        void OnShoot()
//        {
//            float fireForce = 20f;
//            //_rigidbody.AddForce(transform.right * fireForce, ForceMode.Impulse);
//            //added to projectile on instantiation
//            GameObject go = Instantiate(projPrefab, firePos.transform.position, Quaternion.identity);
//            Rigidbody rb = go.AddComponent<Rigidbody>();
//            rb.useGravity = true;
//            rb.isKinematic = false;
//            rb.AddForce(this.transform.right * fireForce, ForceMode.Impulse);
            
//        }
       
//        private void OnCollisionEnter(Collision collision)
//        {
//            if (xVelocity > 100f)
//            {
//                _rigidbody.velocity.Set(0, 0, 0);
//                xVelocity = 0;
//                this.gameObject.SetActive(false);
//            }
           
//        }
//    }
//}
