using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib;
using UnboundLib.Extensions;

namespace DanModCards.Effects
{
    /// <summary>
    /// Infinity Mirror:
    /// - Every real shot spawns 2 child shots.
    /// - Child shots recurse with half the previous multiplier.
    /// - Total spawned child bullets are capped.
    /// - Avoids re-entrant Attack() recursion by running only from the original shot callback.
    /// </summary>
    public class InfinityMirrorEffect : MonoBehaviour
    {
        private const int MaxSpawnedBullets = 30;
        private const float ChildDamageMultiplier = 0.5f;
        private const float ChildAngleOffset = 12.5f;

        private Gun gun = null!;
        private GunAmmo gunAmmo = null!;

        private bool isSpawningChildren;
        private int spawnedBulletsThisShot;

        private void Awake()
        {
            gun = GetComponent<Gun>();
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
            yield return null;

            if (gun == null)
                yield break;

            if (spawnedBulletsThisShot >= MaxSpawnedBullets)
                yield break;

            int savedProjectiles = gun.numberOfProjectiles;
            gun.numberOfProjectiles = 1;

            for (int i = 0; i < 2; i++)
            {
                if (spawnedBulletsThisShot >= MaxSpawnedBullets)
                    break;

                spawnedBulletsThisShot++;

                Vector3 direction = gun.transform.forward;
                float angle = i == 0 ? -ChildAngleOffset : ChildAngleOffset;

                gun.SetFieldValue("forceShootDir", Quaternion.Euler(0f, 0f, angle) * direction);

                int? savedAmmo = gunAmmo != null
                    ? (int?)gunAmmo.GetFieldValue("currentAmmo")
                    : null;

                isSpawningChildren = true;
                gun.Attack(0f, true);
                isSpawningChildren = false;

                if (savedAmmo.HasValue && gunAmmo != null)
                {
                    gunAmmo.SetFieldValue("currentAmmo", savedAmmo.Value);
                }

                var projectile = gun.GetComponentInChildren<ProjectileHit>();
                if (projectile != null)
                {
                    projectile.damage *= damageMultiplier;
                }

                yield return null;
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
