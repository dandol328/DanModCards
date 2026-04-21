using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Continuously fires while the player holds the fire button.
    /// Uses a frame-independent shot budget so extremely fast fire rates are not bottlenecked by frame timing.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        private const float MinimumFireInterval = 0.0005f;
        private const int MaxShotsPerFrame = 256;

        private Gun gun;
        private float fireTimer;

        private void Awake()
        {
            gun = GetComponent<Gun>();
        }

        private void Update()
        {
            if (gun == null) return;

            if (!Input.GetMouseButton(0) || gun.isReloading)
            {
                fireTimer = 0f;
                return;
            }

            float fireInterval = GetFireInterval();
            fireTimer += Time.deltaTime;

            int shotsThisFrame = 0;
            while (fireTimer >= fireInterval && shotsThisFrame < MaxShotsPerFrame)
            {
                if (!gun.Attack(0f, true))
                {
                    fireTimer = Mathf.Min(fireTimer, fireInterval);
                    return;
                }

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
            float cooldown = gun.lockGunToDefault ? gun.defaultCooldown : gun.attackSpeed;
            float attackSpeedMultiplier = Mathf.Max(gun.attackSpeedMultiplier, Mathf.Epsilon);
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
