using UnityEngine;

namespace Avega.LootLogic
{
    public class LootDataGiver
    {
        private readonly LootDataContainer _dataContainer;

        public LootDataGiver(LootDataContainer dataContainer)
        {
            _dataContainer = dataContainer;
        }

        public LootData Get()
        {
            var lootDatas = _dataContainer.LootDatas;

            if (lootDatas.Length == 0)
            {
                Debug.LogError("Empty lootcontainer");
                return null;
            }

            int randomIndex = Random.Range(0, lootDatas.Length);
            LootData lootData = lootDatas[randomIndex];

            return lootData;
        }
    }
}