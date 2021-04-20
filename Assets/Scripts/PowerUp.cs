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
        private float _speed => _powerUpType.speed;
       
        private void Update()
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (transform.position.x < -10f)
            {
                Destroy(this.gameObject);
            }
        }
        //test
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                _collectEvent.RaiseCollectEvent(_powerUpType.powerUpID);
               GameObject collect = Instantiate(_powerUpType.colectedAnimPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
