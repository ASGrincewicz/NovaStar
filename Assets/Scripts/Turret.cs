using System.Collections;
using UnityEngine;
namespace Veganimus.NovaStar
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed = 200f;
        [SerializeField] private float _xRotation;
        [SerializeField] private float _yRotation;
        [SerializeField] private float _zRotation;
        [SerializeField] private bool isPaired = false;
        [SerializeField] private GameObject _pairedObj;
        [SerializeField] private GameObject _turretProjectile;
        [SerializeField] private GameObject _target;
        private Vector3 _myPos;

        private void Start()
        {
            if (isPaired == true)
                _myPos = _pairedObj.transform.position;

            else
                _myPos = transform.position;

            StartCoroutine(TurretFireRoutine());
        }
        private void Update() => TurretRotation();

        private void TurretRotation()
        {
            if (_target != null)
            {
                Vector3 _targetLocation = _target.transform.position;
                _targetLocation.z = _myPos.z; // no 3D rotation
                _targetLocation.x = _myPos.x;
                Vector3 vectorToTarget = _targetLocation - _myPos;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0,_zRotation) * vectorToTarget;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, rotatedVectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);
            }
            else
                return;
        }
        //private void OnTriggerEnter(Collider other)
        //{
        //    if (other.tag == _target.tag && isPaired == true)
        //        _myPos = this.transform.position;

        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    if (other.tag == _target.tag && isPaired == true)
        //        _myPos = _pairedObj.transform.position;

        //}
        private IEnumerator TurretFireRoutine()
        {
            yield return null;
        }
    }
}

