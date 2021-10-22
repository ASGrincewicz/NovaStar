// Aaron Grincewicz ASGrincewicz@icloud.com 10/18/2021
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Grincewicz.SpawnSystem
{
    public class NewSpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform _spawnContainer;
        [SerializeField] private List<Wave> _waveSequence;
        [SerializeField] private Wave _singleWave;
        [SerializeField] private SpawnBounds2D _spawnBound2D;
        [SerializeField] private SpawnBounds3D _spawnBounds3D;
        [SerializeField] private int _currentWave = 0;

        private void Start()
        {
            StartCoroutine(SpawnRoutineSequence(_waveSequence));
            StartCoroutine(SpawnRoutineSingle(_singleWave));
        }
        /// <summary>
        /// This method returns the current 3D boundaries.
        /// </summary>
        /// <returns></returns>
        private Vector3 Get3DBounds()
        {
            var bounds = _spawnBounds3D;
            return new Vector3(
                    Random.Range(bounds.xBounds.x, bounds.xBounds.y),
                    Random.Range(bounds.yBounds.x, bounds.yBounds.y),
                    Random.Range(bounds.zBounds.x, bounds.zBounds.y));
        }
        /// <summary>
        /// This method returns the current 2D boundaries.
        /// </summary>
        /// <returns></returns>
        private Vector3 Get2DBounds()
        {
            var bounds = _spawnBound2D;
            return new Vector3(
           Random.Range(bounds.leftBoundary, bounds.rightBoundary),
           Random.Range(bounds.topBoundary, bounds.bottomBoundary),0);
        }

        /// <summary>
        /// Start this Coroutine to spawn a single wave in a 3D space.
        /// </summary>
        /// <returns></returns>
        public IEnumerator SpawnRoutineSingle(Wave wave)
        {
           
            GameObject spawnedObject = null;
            wave.SpawnInterval = new WaitForSeconds(wave.SpawnDelay);
            for(int i = 0; i < wave.SpawnableObjects.Count; i++)
            {
                yield return wave.SpawnInterval;
                if (wave.IsRandom)
                {
                    spawnedObject = Instantiate(wave.SpawnableObjects
                        [Random.Range(0, wave.SpawnableObjects.Count -1)],_spawnContainer);
                }
                else
                {
                    spawnedObject = Instantiate(wave.SpawnableObjects[i], _spawnContainer);
                }
                if(wave.Is3D)
                {
                    spawnedObject.transform.position = Get3DBounds();
                }
                else
                {
                    spawnedObject.transform.position = Get2DBounds();
                }
            }
        }
        /// <summary>
        /// Start this Coroutine to spawn multiple waves.
        /// </summary>
        /// <returns></returns>
        public IEnumerator SpawnRoutineSequence(List<Wave> sequence)
        {
            GameObject spawnedObject = null;
            Wave activeWave = sequence[_currentWave];
            activeWave.SpawnInterval = new WaitForSeconds(activeWave.SpawnDelay);
            for (int i = 0; i < activeWave.SpawnableObjects.Count; i++)
            {
                yield return activeWave.SpawnInterval;
                if (_waveSequence[_currentWave].IsRandom)
                {
                    spawnedObject = Instantiate(activeWave.SpawnableObjects
                        [Random.Range(0, activeWave.SpawnableObjects.Count - 1)], _spawnContainer);
                }
                else
                {
                    spawnedObject = Instantiate(activeWave.SpawnableObjects[i], _spawnContainer);
                }
                if (activeWave.Is3D)
                {
                    spawnedObject.transform.position = Get3DBounds();
                }
                else
                {
                    spawnedObject.transform.position = Get2DBounds();
                }
            }
            _currentWave++;
            if (_currentWave < sequence.Count)
                StartCoroutine(SpawnRoutineSequence(sequence));
            else
                Debug.Log("Sequence Complete");
        }
    }
}
