using System;
using UnityEngine;

namespace Avega.Utils
{
    public class TriggerEnter : MonoBehaviour
    {
        public event Action<Collider> TriggerEntered;

        private void OnTriggerEnter(Collider other) => 
            TriggerEntered?.Invoke(other);
    }
}