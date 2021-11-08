using System;
using UnityEngine;

namespace Avega
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _value;

        public int Value => _value;

        public event Action<int> Changed;
        public event Action Empty;
        
        public void ApplyDamage(int damage)
        {
            _value -= damage;
            
            if (_value <= 0)
            {
                _value = 0;
                Empty?.Invoke();
            }

            Changed?.Invoke(_value);
        }
        
    }
}