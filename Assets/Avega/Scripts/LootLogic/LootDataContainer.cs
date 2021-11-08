using UnityEngine;

namespace Avega.LootLogic
{
    [CreateAssetMenu(menuName = "LootDataContainer", fileName = "LootDataContainer", order = 0)]
    public class LootDataContainer : ScriptableObject
    {
        public LootData[] LootDatas;
    }
}