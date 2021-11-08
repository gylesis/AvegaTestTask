using System;
using Random = UnityEngine.Random;

namespace Avega.Utils
{
    [Serializable]
    public class MinMaxValue
    {
        public float Min;
        public float Max;

        public float GetValue() => 
            Random.Range(Min, Max);
    }
}