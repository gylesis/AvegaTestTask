using Avega.Utils;
using UnityEngine;

namespace Avega.Mobs
{
    [CreateAssetMenu(fileName = "MobSpawnData", menuName = "MobSpawnData", order = 0)]
    public class MobSpawnData : ScriptableObject
    {
        public MinMaxValue SpawnDistance;
        public float Period = 3;
    }
}