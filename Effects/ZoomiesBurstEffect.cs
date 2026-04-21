using System.Collections;
using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Periodically grants the player an uncontrollable burst of speed and jump power
    /// for a short duration. The interval between bursts is randomised so the player
    /// cannot predict or control when it fires.
    /// </summary>
    public class ZoomiesBurstEffect : MonoBehaviour
    {
        // How much to multiply movement speed during a burst (2.5× = +150 %)
        private const float SpeedMultiplier  = 2.5f;
        // How much to multiply jump height during a burst (2.0× = +100 %)
        private const float JumpMultiplier   = 2.0f;
        // How long each burst lasts (seconds)
        private const float BurstDuration    = 1.5f;
        // Minimum time between bursts (seconds)
        private const float MinInterval      = 4f;
        // Maximum time between bursts (seconds)
        private const float MaxInterval      = 9f;

        private CharacterStatModifiers statModifiers = null!;
        private bool burstActive;

        private void Start()
        {
            statModifiers = GetComponent<CharacterStatModifiers>();
            StartCoroutine(BurstCycle());
        }

        /// <summary>
        /// Continuously waits a random interval then triggers a burst. A new interval is
        /// chosen after each burst completes, keeping the timing genuinely random.
        /// </summary>
        private IEnumerator BurstCycle()
        {
            while (true)
            {
                float waitTime = Random.Range(MinInterval, MaxInterval);
                yield return new WaitForSeconds(waitTime);

                if (!burstActive)
                {
                    StartCoroutine(ApplyBurst());
                }
            }
        }

        private IEnumerator ApplyBurst()
        {
            burstActive = true;

            statModifiers.movementSpeed *= SpeedMultiplier;
            statModifiers.jump          *= JumpMultiplier;

            yield return new WaitForSeconds(BurstDuration);

            statModifiers.movementSpeed /= SpeedMultiplier;
            statModifiers.jump          /= JumpMultiplier;

            burstActive = false;
        }

        private void OnDestroy()
        {
            // If the component is removed while a burst is active, restore the base multipliers
            // so the player is not left with permanently boosted stats.
            if (burstActive)
            {
                statModifiers.movementSpeed /= SpeedMultiplier;
                statModifiers.jump          /= JumpMultiplier;
            }
        }
    }
}
