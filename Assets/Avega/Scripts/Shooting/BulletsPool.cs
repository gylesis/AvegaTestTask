using System.Collections.Generic;
using UnityEngine;

namespace Avega.Shooting
{
    public class BulletsPool
    {
        private readonly Stack<Bullet> _availableBullets = new Stack<Bullet>();

        public BulletsPool(Bullet bulletPrefab, int count = 15)
        {
            for (int i = 0; i < count; i++)
            {
                Bullet bullet = Object.Instantiate(bulletPrefab);

                DisableBullet(bullet);
                
                _availableBullets.Push(bullet);
            }
        }

        public Bullet GetBullet()
        {
            Bullet bullet = _availableBullets.Pop();

            EnableBullet(bullet);   
            bullet.Disabled += OnBulletDisabled;
            
            return bullet;
        }

        private void OnBulletDisabled(Bullet bullet)
        {
            bullet.Disabled -= OnBulletDisabled;

            DisableBullet(bullet);
            
            _availableBullets.Push(bullet);
        }


        private void DisableBullet(Bullet bullet)
        {
            bullet.Disable();
            bullet.enabled = false;
        }
        
        private void EnableBullet(Bullet bullet)
        {
            bullet.enabled = true;
            bullet.Enable();
        }
        
    }
}