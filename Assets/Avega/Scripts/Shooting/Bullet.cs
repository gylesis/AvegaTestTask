using System;
using Avega.Extensions;
using Avega.Mobs;
using Avega.Utils;
using UnityEngine;

namespace Avega.Shooting
{
    public class Bullet : MonoBehaviour
    {
        private const int DISABLE_TIME = 2;
        
        [SerializeField] private Renderer _renderer;
        [SerializeField] private TriggerEnter _triggerEnter;

        private Collider _collider;
        
        public event Action<Bullet> Disabled;
        
        private float _speed;
        private float _timer;
        private int _damage;

        private void Awake()
        {
            _collider = _triggerEnter.gameObject.GetComponent<Collider>();
        }

        public void Init(BulletContext context)
        {
            _triggerEnter.TriggerEntered += OnTriggerEntered;
            _timer = 0;

            _damage = context.Damage;
            Vector3 direction = context.Direction;
            float speed = context.Speed;
            Color color = context.Color;

            _renderer.material.color = color;
            _speed = speed;
            transform.rotation = Quaternion.LookRotation(direction);
        }

        private void OnTriggerEntered(Collider other)
        {
            var hasTag = other.gameObject.TryGetComponentInAllObject<Tag>(out var tag);

            if (hasTag)
            {
                var isMobTag = tag.TagType == TagType.Mob;

                if (isMobTag == false) return;

                var hasHealthComponent = other.gameObject.TryGetComponentInAllObject<Health>(out var health);

                if (hasHealthComponent)
                {
                    health.ApplyDamage(_damage);
                }
            }
            
            Disabled?.Invoke(this);
            
            _triggerEnter.TriggerEntered -= OnTriggerEntered;
        }

        private void Update()
        {
            Move();
            
            CheckDisableCondition();
        }

        private void CheckDisableCondition()
        {
            _timer += Time.deltaTime;

            if (_timer >= DISABLE_TIME)
            {
                _timer = 0;
                Disabled?.Invoke(this);

                _triggerEnter.TriggerEntered -= OnTriggerEntered;
            }
        }

        private void Move()
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }

        public void Enable()
        {
            _collider.enabled = true;
            _renderer.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
            _renderer.enabled = false;
        }
        
        
    }
}