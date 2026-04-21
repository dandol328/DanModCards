using System.Collections.Generic;
using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Subscribes to the player's gun and intercepts bullets as they're fired.
    /// For each bullet, spawns 2 additional bullets recursively up to a maximum of 30 total bullets.
    /// Secondary bullets deal half damage compared to the original bullet.
    /// </summary>
    public class InfinityMirrorEffect : MonoBehaviour
    {
        private const int MaxBullets = 30;
        private int bulletCount = 0;

        private Gun ownerGun = null!;

        private void Start()
        {
            ownerGun = GetComponent<Gun>();
            
            // Hook into the gun's bullet fire event
            if (ownerGun != null)
            {
                ownerGun.gunAmmo.OnShoot += OnBulletFired;
            }
        }

        private void OnDestroy()
        {
            if (ownerGun != null && ownerGun.gunAmmo != null)
            {
                ownerGun.gunAmmo.OnShoot -= OnBulletFired;
            }
        }

        /// <summary>Called every time a bullet is fired.</summary>
        private void OnBulletFired(ProjectileData projectileData)
        {
            // Reset bullet count at the start of each shot
            bulletCount = 0;
            SpawnChildBullets(projectileData, 1f);
        }

        /// <summary>Recursively spawns child bullets up to the maximum limit.</summary>
        private void SpawnChildBullets(ProjectileData parentBullet, float damageMultiplier)
        {
            // Check if we've hit the max bullet cap
            if (bulletCount >= MaxBullets)
            {
                return;
            }

            // Spawn 2 child bullets
            for (int i = 0; i < 2; i++)
            {
                if (bulletCount >= MaxBullets)
                {
                    return;
                }

                bulletCount++;

                // Create a new projectile data for the child bullet
                ProjectileData childBullet = new ProjectileData(parentBullet);
                
                // Reduce damage by half for secondary bullets
                childBullet.damage *= damageMultiplier * 0.5f;

                // Slightly vary the direction to avoid perfect overlap
                Vector3 direction = parentBullet.direction;
                float angleOffset = (i == 0 ? -15f : 15f); // Spread the child bullets
                direction = Quaternion.Euler(0, 0, angleOffset) * direction;
                childBullet.direction = direction;

                // Spawn the child bullet
                if (ownerGun != null)
                {
                    GameObject childBulletObject = Instantiate(parentBullet.gameObject, parentBullet.transform.position, Quaternion.identity);
                    Projectile childProjectile = childBulletObject.GetComponent<Projectile>();
                    
                    if (childProjectile != null)
                    {
                        childProjectile.SetData(childBullet);
                        
                        // Recursively spawn more bullets from this child
                        SpawnChildBullets(childBullet, damageMultiplier * 0.5f);
                    }
                }
            }
        }
    }
}