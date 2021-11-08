using TMPro;
using UnityEngine;

namespace Avega.Player
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            _health.Changed += UpdateText;
            UpdateText(_health.Value);
        }

        private void UpdateText(int value)
        {
            _text.text = $"Health : {value}";
        }
    }
}