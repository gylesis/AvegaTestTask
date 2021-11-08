using Avega.LootLogic;
using UnityEngine;

namespace Avega.Shooting
{
    public class BulletsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnTransform;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _bulletSpeed = 0.5f;
        
        private LootPickUpService _lootPickUpService;
        private BulletsPool _bulletsPool;

        public void Init(LootPickUpService lootPickUpService)
        {
            _bulletsPool = new BulletsPool(_bulletPrefab);
            _lootPickUpService = lootPickUpService;
        }

        public Bullet Spawn(Vector3 direction, int damage)
        {
            Bullet bullet = _bulletsPool.GetBullet();

            bullet.transform.position = _spawnTransform.position;
            
            var bulletContext = new BulletContext();

            bulletContext.Damage = damage;
            bulletContext.Color = _lootPickUpService.GetLastColor();
            bulletContext.Direction = direction;
            bulletContext.Speed = _bulletSpeed;
            
            bullet.Init(bulletContext);

            return bullet;
        }

    }
}