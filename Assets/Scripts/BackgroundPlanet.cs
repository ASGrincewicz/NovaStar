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
        private int _planetToSpawn = 0;

        private void Start()
        {
            _currentPlanet = _planets[0];
        }

        private void Update()
        {
            _currentPlanet.transform.Translate(Vector3.left * _speed * Time.deltaTime);
            if (_currentPlanet.transform.position.x < -15f)
            {
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
                planet.transform.position = transform.position;
                float yPos = planet.transform.position.y;
                yPos = Random.Range(-3, 5);
                planet.transform.rotation = Quaternion.identity;
                planet.SetActive(true);
                _currentPlanet = planet;
            }
        }
    }
}
