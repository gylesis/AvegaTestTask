using System.Collections.Generic;
using UnityEngine;

namespace Avega.LootLogic
{
    public class LootCountViewService : MonoBehaviour
    {
        [SerializeField] private List<LootCountView> _lootCountViews;

        public void Init(LootContainer lootContainer, LootDataContainer lootDataContainer)
        {
            for (var index = 0; index < lootDataContainer.LootDatas.Length; index++)
            {
                LootData lootData = lootDataContainer.LootDatas[index];
                _lootCountViews[index].Init(lootData.Color);
            }

            lootContainer.Stored += OnLootStored;
        }

        private void OnLootStored(Color color, int count)
        {
            foreach (LootCountView lootCountView in _lootCountViews)
            {
                var compareColor = lootCountView.CompareColor(color);

                if (compareColor)
                {
                    lootCountView.UpdateText(count);
                }
            }
        }
    }
}