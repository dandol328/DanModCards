using System;
using System.Collections;
using UnityEngine;
using UnboundLib;
using UnboundLib.Extensions;

namespace DanModCards.Effects
{
    /// <summary>
    /// Infinity Mirror:
    /// - Every real shot spawns 2 child shots with a visible delay.
    /// - Child shots recurse with half the previous damage multiplier.
    /// - Total spawned child bullets are capped at <see cref="MaxSpawnedBullets"/>.
    /// - Uses gun.transform.up as the 2D aim direction to give child bullets a proper path.
    /// </summary>
    public class InfinityMirrorEffect : MonoBehaviour
    {
        private const int   MaxSpawnedBullets    = 30;
        private const float ChildDamageMultiplier = 0.5f;
        private const float ChildAngleOffset      = 12.5f;
        private const float ChildSpawnDelay       = 0.15f;

        private Gun     gun     = null!;
        private GunAmmo gunAmmo = null!;

        private bool isSpawningChildren;
        private int  spawnedBulletsThisShot;

        private void Awake()
        {
            gun     = GetComponent<Gun>();
            gunAmmo = GetComponentInChildren<GunAmmo>();
        }

        private void Start()
        {
            if (gun != null)
            {
                gun.ShootPojectileAction += OnShootProjectile;
            }
        }

        private void OnDestroy()
        {
            if (gun != null)
            {
                gun.ShootPojectileAction -= OnShootProjectile;
            }
        }

        private void OnShootProjectile(GameObject projectile)
        {
            // Ignore child-triggered shots; only the player's actual shot starts the chain.
            if (isSpawningChildren)
                return;

            spawnedBulletsThisShot = 0;
            StartCoroutine(SpawnChildChain(ChildDamageMultiplier));
        }

        private IEnumerator SpawnChildChain(float damageMultiplier)
        {
            // Visible delay so children appear after the parent bullet.
            yield return new WaitForSeconds(ChildSpawnDelay);

            if (gun == null)
                yield break;

            if (spawnedBulletsThisShot >= MaxSpawnedBullets)
                yield break;

            int savedProjectiles = gun.numberOfProjectiles;
            gun.numberOfProjectiles = 1;

            // Use the gun's local-up direction (the 2D aim direction in ROUNDS).
            Vector3 aimDir = gun.transform.up;

            for (int i = 0; i < 2; i++)
            {
                if (spawnedBulletsThisShot >= MaxSpawnedBullets)
                    break;

                spawnedBulletsThisShot++;

                float angle = i == 0 ? -ChildAngleOffset : ChildAngleOffset;
                gun.SetFieldValue("forceShootDir", Quaternion.Euler(0f, 0f, angle) * aimDir);

                int? savedAmmo = gunAmmo != null
                    ? (int?)gunAmmo.GetFieldValue("currentAmmo")
                    : null;

                // Capture the projectile spawned by this Attack() call so we can apply damage.
                GameObject spawnedProjectile = null;
                Action<GameObject> capture   = proj => spawnedProjectile = proj;
                gun.ShootPojectileAction     += capture;

                isSpawningChildren = true;
                gun.Attack(0f, true);
                isSpawningChildren = false;

                gun.ShootPojectileAction -= capture;

                if (savedAmmo.HasValue && gunAmmo != null)
                {
                    gunAmmo.SetFieldValue("currentAmmo", savedAmmo.Value);
                }

                if (spawnedProjectile != null)
                {
                    var hit = spawnedProjectile.GetComponentInChildren<ProjectileHit>();
                    if (hit != null)
                    {
                        hit.damage *= damageMultiplier;
                    }
                }

                // Brief pause between the two sibling child bullets.
                yield return new WaitForSeconds(ChildSpawnDelay);
            }

            gun.SetFieldValue("forceShootDir", Vector3.zero);
            gun.numberOfProjectiles = savedProjectiles;

            if (spawnedBulletsThisShot < MaxSpawnedBullets)
            {
                StartCoroutine(SpawnChildChain(damageMultiplier * ChildDamageMultiplier));
            }
        }
    }
}
