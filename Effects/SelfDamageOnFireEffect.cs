using UnboundLib;
using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Deals a percentage of the player's max health as self-damage each time a bullet is fired.
    /// Attach to the gun's GameObject. Set <see cref="DamagePercent"/> before or after attaching.
    /// </summary>
    public class SelfDamageOnFireEffect : MonoBehaviour
    {
        /// <summary>Fraction of max health dealt as self-damage per shot fired (e.g. 0.30f = 30%).</summary>
        public float DamagePercent = 0.30f;

        private GunAmmo gunAmmo;
        private HealthHandler healthHandler;
        private CharacterData characterData;
        private int previousAmmo = -1;

        private void Start()
        {
            gunAmmo = GetComponentInParent<GunAmmo>();
            var player = GetComponentInParent<Player>();
            if (player != null)
            {
                healthHandler  = player.GetComponent<HealthHandler>();
                characterData  = player.GetComponent<CharacterData>();
            }

            if (gunAmmo != null)
                previousAmmo = (int)gunAmmo.GetFieldValue("currentAmmo");
        }

        private void Update()
        {
            if (gunAmmo == null || healthHandler == null || characterData == null || previousAmmo < 0)
                return;

            int current = (int)gunAmmo.GetFieldValue("currentAmmo");
            if (current < previousAmmo)
            {
                int shots = previousAmmo - current;
                float dmg  = characterData.maxHealth * DamagePercent * shots;
                healthHandler.CallTakeDamage(
                    new Vector2(dmg, 0f), transform.position, null, null, false);
            }

            previousAmmo = current;
        }
    }
}
