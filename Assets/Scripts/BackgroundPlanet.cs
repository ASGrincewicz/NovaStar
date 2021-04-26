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
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private float _speed;
        [SerializeField] private List<GameObject> _planets = new List<GameObject>();
        [SerializeField] private GameObject _currentPlanet;
        private int _planetToSpawn = 0;

        private void Start()=> _currentPlanet = _planets[0];

        private void Update()
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (transform.position.x < -15f)
            {
                transform.position = _startPosition;
                _currentPlanet.SetActive(false);
                SpawnPlanet();
            }
        }
        private void SpawnPlanet()
        {
            if (_planetToSpawn > _planets.Count)
                _planetToSpawn = 0;

            else
            {
                _planetToSpawn++;
                GameObject planet = _planets[_planetToSpawn];
                //planet.transform.position = transform.position;
                planet.SetActive(true);
                _currentPlanet = planet;
            }
        }
    }
}
