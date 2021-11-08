using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Avega.Mobs
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private bool _spawnOn = true;

        private MobSpawnData _spawnData;
        private Transform _player;
        private MobFactory _factory;

        private float _timer;
        private Camera _camera;

        public void Init(Transform player, MobSpawnData spawnData, MobFactory factory, Camera camera)
        {
            _camera = camera;
            _factory = factory;
            _player = player;
            _spawnData = spawnData;
        }

        private void Update()
        {
            if (_spawnOn == false) return;

            _timer += Time.deltaTime;

            if (_timer >= _spawnData.Period)
            {
                _timer = 0;
                SpawnMob();
            }
        }

        private void SpawnMob()
        {
            var spawnPos = GetSpawnPos();

            var spawnContext = new MobSpawnContext();

            spawnContext.Position = spawnPos;

            _factory.Create(spawnContext);
        }

        private Vector3 GetSpawnPos()
        {
            var fieldOfView = _camera.fieldOfView;

            float randomAngle = Random.Range(fieldOfView, 360 - fieldOfView);

            var cameraPos = Vector3.ProjectOnPlane( _camera.transform.position, Vector3.up);

            Vector3 randomDirection = (_camera.transform.forward) * Mathf.Cos(randomAngle * Mathf.Deg2Rad) +
                                      (_camera.transform.right) * Mathf.Sin(randomAngle * Mathf.Deg2Rad) + 
                                      cameraPos;

            var randomSpawnDistance = _spawnData.SpawnDistance.GetValue();

            Vector3 spawnPos = (randomDirection - cameraPos) * randomSpawnDistance + randomDirection;
            
            if (NavMesh.SamplePosition(spawnPos, out var hit, float.MaxValue, NavMesh.AllAreas))
            {
                spawnPos = hit.position;
            }
            
            return spawnPos;
        }
        
    }
}