using System;
using Avega.Extensions;
using Avega.LootLogic;
using Avega.Mobs;
using Avega.Player;
using Avega.Shooting;
using UnityEngine;

namespace Avega
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private LootPickUpService _lootPickUpService;

        [SerializeField] private BulletsSpawner _bulletsSpawner;

        [SerializeField] private LootCountViewService _lootCountViewService;
        [SerializeField] private LootDataContainer _lootDataContainer;
        [SerializeField] private Loot _lootPrefab;

        [SerializeField] private Weapon _weapon;

        [SerializeField] private Transform _player;

        [SerializeField] private Mob _mobPrefab;
        [SerializeField] private MobSpawner _mobSpawner;
        [SerializeField] private MobSpawnData _mobSpawnData;
        [SerializeField] private MobAttackData _mobAttackData;

        [SerializeField] private Camera _camera;

        [SerializeField] private float _angle;
        
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            var lootContainer = new LootContainer();

            _lootCountViewService.Init(lootContainer, _lootDataContainer);
            _lootPickUpService.Init(lootContainer);

            _bulletsSpawner.Init(_lootPickUpService);

            _player.gameObject.TryGetComponentInAllObject<Health>(out var health);
            new PlayerDeathService(health);

            var lootContextGiver = new LootDataGiver(_lootDataContainer);

            var lootSpawner = new LootSpawner(lootContextGiver, _lootPrefab);

            var mobDeathsService = new MobDeathsService(lootSpawner);

            var mobFactory = new MobFactory(_mobPrefab, mobDeathsService, _player, _mobAttackData);

            _mobSpawner.Init(_player, _mobSpawnData, mobFactory, _camera);

            _weapon.Init(_bulletsSpawner);
        }

        private void OnDrawGizmos()
        {
            Vector3 vector1 = _camera.transform.forward * Mathf.Cos(_angle * Mathf.Deg2Rad) +
                                        _camera.transform.right * Mathf.Sin(_angle * Mathf.Deg2Rad) +
                                        _camera.transform.position;
            
            /*Vector3 vector2 = _camera.transform.forward * Mathf.Cos(_angle * Mathf.Deg2Rad) +
                              _camera.transform.right * -Mathf.Sin(_angle * Mathf.Deg2Rad) +
                              _camera.transform.position;*/

            Gizmos.color = Color.black;
            Gizmos.DrawLine(_camera.transform.position, vector1);

            /*Gizmos.color = Color.blue;
            Gizmos.DrawLine(_camera.transform.position, vector2);*/
        }
    }
}