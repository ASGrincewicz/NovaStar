using System.Collections.Generic;
using UnityEngine;
using Grincewicz.PoolManager;
namespace Veganimus.NovaStar
{
    public class PowerUpPoolManager : MonoBehaviour
    {
        [Header("Power Up Pool")]
        [SerializeField] private List<GameObject> _powerUps;
        [SerializeField] private GameObject _powerUpPrefab;
        [SerializeField] private Transform _powerUpContainer;
        [Header("Listening to:")]
        [SerializeField] private PoolGORequest _powerUpRequest;

        private void OnEnable() => _powerUpRequest.OnGameObjectIntRequested += RequestPowerUp;

        private void OnDisable() => _powerUpRequest.OnGameObjectIntRequested -= RequestPowerUp;

        private void Start() => _powerUpPrefab = _powerUps[0];

        private GameObject RequestPowerUp(int scoreTier)
        {
            _powerUpPrefab = _powerUps[Random.Range(0, 3)];
            GameObject powerUp = Instantiate(_powerUpPrefab, _powerUpContainer);
            return powerUp;
        }
    }
}
