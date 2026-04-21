using UnboundLib;
using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Adds pierce to every projectile fired by the attached gun by modifying the
    /// spawned <see cref="ProjectileHit"/> component. Attach to the gun's GameObject
    /// and set <see cref="PierceCount"/> before or after attaching.
    /// </summary>
    public class PierceEffect : MonoBehaviour
    {
        public int PierceCount = 1;

        private Gun gun;

        private void Start()
        {
            gun = GetComponent<Gun>();
            if (gun != null)
                gun.ShootPojectileAction += OnShootProjectile;
        }

        private void OnDestroy()
        {
            if (gun != null)
                gun.ShootPojectileAction -= OnShootProjectile;
        }

        private void OnShootProjectile(GameObject projectile)
        {
            var hit = projectile.GetComponentInChildren<ProjectileHit>();
            if (hit == null) return;

            int current = 0;
            try { current = (int)hit.GetFieldValue("nrOfPierces"); }
            catch { }

            hit.SetFieldValue("nrOfPierces", current + PierceCount);
        }
    }
}
