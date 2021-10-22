using UnityEngine;
using Grincewicz.PoolManager;
namespace Veganimus.NovaStar
{
    /// <summary>
    /// Extends the Pool Manager Class to allow the current weapon projectile to be pooled.
    /// However, it does NOT inherit from PoolManager. You must assign the Pool Manager field.
    /// </summary>
    public class PlayerWeaponUpdater : MonoBehaviour
    {
        [SerializeField] private NewPoolManager _poolManager;
        [SerializeField] private PlayerWeaponEvent _playerWeaponEvent;

        private void OnEnable() => _playerWeaponEvent.OnPlayerWeaponChangeEventRaised += GetCurrentWeapon;

        private void OnDisable() => _playerWeaponEvent.OnPlayerWeaponChangeEventRaised -= GetCurrentWeapon;
        /// <summary>
        /// This method is called by an event when the Player's weapon changes.
        /// </summary>
        /// <param name="weapon"></param>
        private void GetCurrentWeapon(WeaponType weapon)
        {
            _poolManager.CreateNewPool(0, weapon.projectilePrefab, 20);
            //_poolManager.PoolableObjectZero = weapon.projectilePrefab;
            //if (_poolManager.PoolableObjectZero != null)
            //    _poolManager.PoolZero = _poolManager.GenerateObjects(_poolManager.PoolZero, _poolManager.PoolableObjectZero, _poolManager.PoolableObjectContainerZero, 20);
        }
    }
}
