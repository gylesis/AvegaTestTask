using System.Collections;
using UnityEngine;

namespace Avega.Shooting
{
    public class Weapon : MonoBehaviour
    {
        public FP_Input playerInput;

        public float shootRate = 0.15F;
        public float reloadTime = 1.0F;
        public int ammoCount = 15;
        public int damage;
        
        private int ammo;
        private float delay;
        private bool reloading;
        private BulletsSpawner bulletsSpawner;

        public void Init(BulletsSpawner bulletsSpawner)
        {
            this.bulletsSpawner = bulletsSpawner;
        }

        void Start()
        {
            ammo = ammoCount;
        }

        void Update()
        {
            bool shoot = playerInput.UseMobileInput ? playerInput.Shoot() : Input.GetMouseButton(0);

            if (shoot)
                if (Time.time > delay)
                    Shoot();


            if (playerInput.Reload() || Input.GetKeyDown(KeyCode.R))      
                if (!reloading && ammoCount < ammo)
                    StartCoroutine("Reload");
        }

        void Shoot()
        {
            if (ammoCount > 0)
            {
                bulletsSpawner.Spawn(transform.forward,damage);
                ammoCount--;
            }
            else
                Debug.Log("Empty");

            delay = Time.time + shootRate;
        }

        IEnumerator Reload()
        {
            reloading = true;
            Debug.Log("Reloading");
            yield return new WaitForSeconds(reloadTime);
            ammoCount = ammo;
            Debug.Log("Reloading Complete");
            reloading = false;
        }

        void OnGUI()
        {
            GUILayout.Label("AMMO: " + ammoCount);
        }
    }
}