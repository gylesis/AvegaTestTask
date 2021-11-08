using UnityEngine;

namespace Avega.LootLogic
{
    public class LootSpawner
    {
        private readonly LootDataGiver _dataGiver;
        private readonly Loot _prefab;

        public LootSpawner(LootDataGiver dataGiver, Loot prefab)
        {
            _prefab = prefab;
            _dataGiver = dataGiver;
        }

        public Loot Spawn(Vector3 position)
        {
            LootData lootData = _dataGiver.Get();
            Loot lootInstance = Object.Instantiate(_prefab,position,Quaternion.identity);
            
            lootInstance.Init(lootData);

            return lootInstance;
        }
        
    }
}