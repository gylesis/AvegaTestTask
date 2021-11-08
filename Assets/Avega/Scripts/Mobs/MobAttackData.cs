using UnityEngine;

namespace Avega.Mobs
{
    [CreateAssetMenu(menuName = "MobAttackData", fileName = "MobAttackData", order = 0)]
    public class MobAttackData : ScriptableObject
    {
        public int Damage = 5;
        public float AttackPeriod = 2;
        public LayerMask AttackableLayers;

        [SerializeField] private float _attackDistance;
        public float AttackDistance => _attackDistance * _attackDistance;
    }
}