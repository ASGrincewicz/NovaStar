using System.Collections.Generic;
using UnityEngine;

namespace Veganimus.NovaStar
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class BackgroundPlanet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private List<GameObject> _planets = new List<GameObject>();
        [SerializeField] private GameObject _currentPlanet;
        private void Start()
        {
            GameObject planet = Instantiate(_planets[UnityEngine.Random.Range(0, _planets.Count -1)], transform.position, Quaternion.identity, this.transform);
            _currentPlanet = planet;
        }

        void Update()
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (transform.position.x < -15f)
             Destroy(gameObject);
        }
    }
}
