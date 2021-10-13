using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private CollectEvent _collectEvent;
        [SerializeField] private PowerUpSO _powerUpType;
        private float _deltaTime;
        private Transform _transform;
        private float _speed => _powerUpType.speed;

        private void Start() => _transform = transform;

        private void Update()
        {
            _deltaTime = Time.deltaTime * _speed;
            _transform.Translate(Vector3.left * _deltaTime);
            if (_transform.localPosition.x < -10f)
             Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _collectEvent.RaiseCollectEvent(_powerUpType.powerUpID);
                Instantiate(_powerUpType.colectedAnimPrefab, _transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
