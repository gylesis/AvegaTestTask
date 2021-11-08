using System;
using System.Collections.Generic;
using UnityEngine;

namespace Avega.LootLogic
{
    public class LootContainer
    {
        private readonly Dictionary<Color, int> _colorCollection = new Dictionary<Color, int>();

        public event Action<Color, int> Stored;

        public void Store(Loot loot)
        {
            Color color = loot.Data.Color;

            if (_colorCollection.ContainsKey(color))
            {
                _colorCollection[color]++;
            }
            else
            {
                _colorCollection.Add(color, 1);
            }

            var count = _colorCollection[color];

            Stored?.Invoke(color, count);
        }
    }
}