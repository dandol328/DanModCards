using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Continuously fires while the player holds the fire button.
    /// Uses a frame-independent shot budget so extremely fast fire rates are not bottlenecked by frame timing.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        // Floors the interval at 0.5ms (2000 shots/sec max) so stacked fire-rate boosts stay responsive without runaway loops.
        private const float MinimumFireInterval = 0.0005f;
        // Prevents a long frame hitch from spawning an unbounded number of attacks in one Update.
        private const int MaxShotsPerFrame = 256;
        // Avoids divide-by-zero and keeps near-zero multipliers from stretching the interval past 100x the base cooldown.
        private const float MinimumAttackSpeedMultiplier = 0.01f;

        private Gun gun;
        private float fireTimer;

        private void Awake()
        {
            gun = GetComponent<Gun>();
        }

        private void Update()
        {
            // Match the original effect's left-mouse auto-fire behavior; this component does not have access to a higher-level input abstraction.
            if (gun == null || !Input.GetMouseButton(0) || gun.isReloading)
            {
                fireTimer = 0f;
                return;
            }

            float fireInterval = GetFireInterval();
            fireTimer += Time.deltaTime;

            int shotsThisFrame = 0;
            while (fireTimer >= fireInterval && shotsThisFrame < MaxShotsPerFrame)
            {
                // Setting charge=0f fires an instant shot, and forceAttack=true intentionally bypasses Gun.sinceAttack because it only advances once per frame.
                // fireTimer provides the real rate limit here, so this removes the framerate bottleneck without making the card unbounded.
                if (!gun.Attack(0f, true))
                {
                    fireTimer = 0f;
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
            if (gun == null)
            {
                return MinimumFireInterval;
            }

            float cooldown = gun.lockGunToDefault ? gun.defaultCooldown : gun.attackSpeed;
            float attackSpeedMultiplier = Mathf.Max(gun.attackSpeedMultiplier, MinimumAttackSpeedMultiplier);
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
