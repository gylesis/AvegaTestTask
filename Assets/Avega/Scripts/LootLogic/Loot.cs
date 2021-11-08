using UnityEngine;

namespace Avega.LootLogic
{
    public class Loot : MonoBehaviour
    {
        public LootData Data { get; private set; }

        [SerializeField] private Renderer _renderer;
        
        public void Init(LootData data)
        {
            Data = data;
            _renderer.material.color = data.Color;
        }
    }
}