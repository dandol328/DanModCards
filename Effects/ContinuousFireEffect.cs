using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Continuously fires while the player holds the fire button.
    /// Uses a frame-independent shot budget so extremely fast fire rates are not bottlenecked by frame timing.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        // 0.00005 = 20,000 shots/sec theoretical max
        private const float MinimumFireInterval = 0.00005f;

        // Prevents insane frame hitch explosions
        private const int MaxShotsPerFrame = 1024;

        private Gun gun;
        private GunAmmo gunAmmo;

        private float fireTimer;

        private void Awake()
        {
            gun = GetComponent<Gun>();
            gunAmmo = GetComponent<GunAmmo>();
        }

        private void Update()
        {
            if (gun == null || !Input.GetMouseButton(0))
            {
                fireTimer = 0f;
                return;
            }

            // 🔥 Force gun to always be ready to fire
            gun.sinceAttack = 999f;

            float fireInterval = GetFireInterval();
            fireTimer += Time.deltaTime;

            int shotsThisFrame = 0;

            while (fireTimer >= fireInterval && shotsThisFrame < MaxShotsPerFrame)
            {
                // Try to fire, but DO NOT early-return if it fails
                gun.Attack(0f, true);

                fireTimer -= fireInterval;
                shotsThisFrame++;
            }

            if (shotsThisFrame == MaxShotsPerFrame)
            {
                fireTimer = Mathf.Min(fireTimer, fireInterval);
            }
        }

        private float GetFireInterval()
        {
            if (gun == null)
                return MinimumFireInterval;

            float cooldown = gun.lockGunToDefault ? gun.defaultCooldown : gun.attackSpeed;

            float attackSpeedMultiplier = Mathf.Max(gun.attackSpeedMultiplier, 0.01f);

            return Mathf.Max(cooldown / attackSpeedMultiplier, MinimumFireInterval);
        }

        private void OnDisable()
        {
            fireTimer = 0f;
        }

        private void OnDestroy()
        {
            fireTimer = 0f;
        }
    }
}
