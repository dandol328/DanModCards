using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib;

namespace DanModCards.Effects
{
    /// <summary>
    /// Subscribes to the player's gun and fires extra bullets whenever a bullet is spawned.
    /// For each bullet fired, spawns 2 additional bullets recursively up to a maximum of 30 total.
    /// Secondary bullets deal half damage compared to their parent bullet.
    /// </summary>
    public class InfinityMirrorEffect : MonoBehaviour
    {
        private const int MaxBullets = 30;

        private Gun ownerGun = null!;
        private GunAmmo ownerGunAmmo = null!;

        private int bulletCount = 0;
        private bool isSpawningChild = false;

        // Queued damage multipliers for the child bullets we are about to spawn
        private readonly Queue<float> pendingDamageMultipliers = new Queue<float>();

        private void Start()
        {
            ownerGun = GetComponent<Gun>();
            ownerGunAmmo = GetComponentInChildren<GunAmmo>();

            if (ownerGun != null)
            {
                ownerGun.ShootPojectileAction += OnBulletFired;
            }
        }

        private void OnDestroy()
        {
            if (ownerGun != null)
            {
                ownerGun.ShootPojectileAction -= OnBulletFired;
            }
        }

        /// <summary>Called every time a bullet is fired by the gun.</summary>
        private void OnBulletFired(GameObject projectile)
        {
            if (!isSpawningChild)
            {
                // Original shot from the player – reset counter for this burst
                bulletCount = 0;
                pendingDamageMultipliers.Clear();

                // Schedule 2 child bullets from this original bullet
                if (bulletCount < MaxBullets)
                {
                    StartCoroutine(SpawnChildBullets(0.5f));
                }
                return;
            }

            // Child bullet – apply its queued damage multiplier
            if (pendingDamageMultipliers.Count > 0)
            {
                float damageMultiplier = pendingDamageMultipliers.Dequeue();
                ProjectileHit hit = projectile.GetComponent<ProjectileHit>();
                if (hit != null)
                {
                    hit.damage *= damageMultiplier;
                }

                // Recursively schedule children for this child bullet
                if (bulletCount < MaxBullets)
                {
                    StartCoroutine(SpawnChildBullets(damageMultiplier * 0.5f));
                }
            }
        }

        /// <summary>Fires 2 child bullets from the owner gun after a single frame delay.</summary>
        private IEnumerator SpawnChildBullets(float damageMultiplier)
        {
            yield return null;

            if (ownerGun == null) yield break;

            int savedProjectiles = ownerGun.numberOfProjectiles;
            ownerGun.numberOfProjectiles = 1;

            for (int i = 0; i < 2; i++)
            {
                if (bulletCount >= MaxBullets) break;

                bulletCount++;
                pendingDamageMultipliers.Enqueue(damageMultiplier);

                // Rotate in the XY plane (Z-axis) — correct for ROUNDS' 2D side-scrolling space.
                Vector3 shootDir = ownerGun.transform.forward;
                float angleOffset = (i == 0) ? -15f : 15f;
                ownerGun.SetFieldValue("forceShootDir",
                    Quaternion.Euler(0f, 0f, angleOffset) * shootDir);

                isSpawningChild = true;

                // Save ammo so child bullets do not consume the player's ammo.
                // Only interact with the ammo system when a GunAmmo component is present.
                int? savedAmmo = ownerGunAmmo != null
                    ? (int?)ownerGunAmmo.GetFieldValue("currentAmmo")
                    : null;

                ownerGun.Attack(0f, true);

                if (savedAmmo.HasValue && ownerGunAmmo != null)
                    ownerGunAmmo.SetFieldValue("currentAmmo", savedAmmo.Value);

                isSpawningChild = false;

                // Restore direction to default (gun's normal aim)
                ownerGun.SetFieldValue("forceShootDir", Vector3.zero);
            }

            ownerGun.numberOfProjectiles = savedProjectiles;
        }
    }
}