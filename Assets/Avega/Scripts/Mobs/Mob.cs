using System;
using Avega.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Avega.Mobs
{
    public class Mob : MonoBehaviour
    {
        private static readonly int AttackTrigger = Animator.StringToHash("Attack");
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Health _health;
        [SerializeField] private Transform _attackTransform;
        [SerializeField] private Animator _animator;

        private float _timer;
        private Transform _player;
        private State _state;
        private MobAttackData _mobAttackData;
        public event Action<Mob> Died;

        public void Init(Transform player, MobAttackData attackData)
        {
            _mobAttackData = attackData;
            _health.Empty += () => Died?.Invoke(this);

            _player = player;
        }

        private void Update()
        {
            _state = CloseToPlayer() ? State.Attack : State.Chase;

            switch (_state)
            {
                case State.Attack:
                    
                    _timer += Time.deltaTime;

                    if (_timer >= _mobAttackData.AttackPeriod)
                    {
                        _timer = 0;
                        Attack();
                    }

                    Vector3 transformPosition = transform.position;
                    transformPosition.y = _player.position.y;

                    Vector3 direction = (_player.position - transformPosition).normalized;   

                    transform.rotation = Quaternion.LookRotation(direction);
                    
                    break;
                
                case State.Chase:
                    
                    _navMeshAgent.SetDestination(_player.position);
                    break;
            }
        }
        private void Attack()
        {
            _animator.SetTrigger(AttackTrigger);
            
            var raycastHits = Physics.SphereCastAll(_attackTransform.position, 0.1f, _attackTransform.forward,
                float.MaxValue, _mobAttackData.AttackableLayers);

            foreach (RaycastHit hit in raycastHits)
            {
                var hasTag = hit.collider.gameObject.TryGetComponentInAllObject<Tag>(out var tag);

                if (hasTag)
                {
                    var isPlayerTag = tag.TagType == TagType.Player;

                    if (isPlayerTag == false) return;

                    Debug.Log(hit.collider);
                    
                    var hasHealthComponent = hit.collider.gameObject.TryGetComponentInAllObject<Health>(out var health);

                    if (hasHealthComponent)
                    {
                        health.ApplyDamage(_mobAttackData.Damage);
                        Debug.Log($"Ебнул по спине");
                    }
                }
            }
        }
        private bool CloseToPlayer()
        {
            float distanceSqrMagnitude = (_player.transform.position - transform.position).sqrMagnitude;

            return distanceSqrMagnitude <= _mobAttackData.AttackDistance;
        }
    }

    public enum State
    {
        Chase,
        Attack
    }
    
}