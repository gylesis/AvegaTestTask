using UnityEngine;

namespace Avega.Mobs
{
    public class MobFactory
    {
        private readonly Mob _mobPrefab;
        private readonly MobDeathsService _mobDeathsService;
        private readonly Transform _player;
        private readonly MobAttackData _mobAttackData;

        public MobFactory(Mob mobPrefab, MobDeathsService mobDeathsService, Transform player,
            MobAttackData mobAttackData)
        {
            _mobAttackData = mobAttackData;
            _player = player;
            _mobDeathsService = mobDeathsService;
            _mobPrefab = mobPrefab;
        }

        public Mob Create(MobSpawnContext context)
        {
            Mob mobInstance = Object.Instantiate(_mobPrefab, context.Position, Quaternion.identity);

            mobInstance.name += $" {mobInstance.GetHashCode()}";

            mobInstance.Init(_player, _mobAttackData);

            _mobDeathsService.AddMob(mobInstance);

            return mobInstance;
        }
    }
}