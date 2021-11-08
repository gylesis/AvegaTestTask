using Avega.Utils;
using UnityEngine;

namespace Avega.LootLogic
{
    public class LootPickUpService : MonoBehaviour
    {
        private Loot _lastPickedLoot;
        private LootContainer _lootContainer;

        [SerializeField] private TriggerEnter _triggerEnter;

        public void Init(LootContainer lootContainer)
        {
            _lootContainer = lootContainer;
        }
        
        private void Awake()
        {
            _triggerEnter.TriggerEntered += OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider collider)
        {
            if (collider.transform.root.TryGetComponent<Loot>(out var loot))
            {
                _lastPickedLoot = loot;
                _lootContainer.Store(loot);
                
                loot.gameObject.SetActive(false);
            }
        }

        public Color GetLastColor()
        {
            if (_lastPickedLoot == null)
                return Color.white;

            return _lastPickedLoot.Data.Color;
        }

        private void OnDestroy()
        {
            _triggerEnter.TriggerEntered -= OnTriggerEntered;
        }
    }
}