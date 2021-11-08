using System.Collections.Generic;
using Avega.LootLogic;
using UnityEngine;

namespace Avega.Mobs
{
    public class MobDeathsService
    {
        private readonly LootSpawner _lootSpawner;
        private List<Mob> _mobs = new List<Mob>();
        
        public MobDeathsService(LootSpawner lootSpawner)
        {
            _lootSpawner = lootSpawner;
        }
        
        public void AddMob(Mob mob)
        {
            _mobs.Add(mob);
            mob.Died += OnMobDied;
        }

        private void OnMobDied(Mob mob)
        {
            Vector3 position = mob.transform.position;
            _lootSpawner.Spawn(position);
            
            mob.gameObject.SetActive(false);
            
            mob.Died -= OnMobDied;
        }
        
    }
}