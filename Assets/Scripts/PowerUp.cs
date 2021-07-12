using UnityEngine;


namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>

    public class PowerUp : MonoBehaviour
    {
        [SerializeField] private PowerUpSO _powerUpType;
        [SerializeField] private CollectEvent _collectEvent;
        private Transform _transform;
        private float _speed => _powerUpType.speed;
        private float _deltaTime;

        private void Start() => _transform = transform;

        private void Update()
        {
            _deltaTime = Time.deltaTime * _speed;
            _transform.Translate(Vector3.left * _deltaTime);
            if (_transform.position.x < -10f)
             Destroy(this.gameObject);
        }
        //test
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _collectEvent.RaiseCollectEvent(_powerUpType.powerUpID);
               GameObject collect = Instantiate(_powerUpType.colectedAnimPrefab, _transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
