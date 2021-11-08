using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Avega.LootLogic
{
    public class LootCountView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _colorImage;

        public void Init(Color color, int value = 0)
        {
            UpdateText(value);
            _colorImage.color = color;
        }

        public void UpdateText(int value)
        {
            _text.text = value.ToString();
        }

        public bool CompareColor(Color color) => color == _colorImage.color;
    }
}