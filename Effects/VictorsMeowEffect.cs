using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Subscribes to the player's Block action and fires a radial shockwave that knocks
    /// back all other players within range whenever a block is performed.
    /// </summary>
    public class VictorsMeowEffect : MonoBehaviour
    {
        // Radius of the shockwave in world units
        private const float ShockwaveRadius    = 6f;
        // Impulse force applied to each hit player
        private const float KnockbackForce     = 35f;

        private Player ownerPlayer = null!;
        private Block  ownerBlock  = null!;

        private void Start()
        {
            ownerPlayer = GetComponent<Player>();
            ownerBlock  = GetComponent<Block>();

            ownerBlock.BlockAction += OnBlock;
        }

        private void OnDestroy()
        {
            if (ownerBlock != null)
            {
                ownerBlock.BlockAction -= OnBlock;
            }
        }

        /// <summary>Called every time the owning player successfully blocks.</summary>
        private void OnBlock(BlockTrigger.BlockTriggerType blockTriggerType)
        {
            Vector2 origin = ownerPlayer.transform.position;

            // Find all colliders within the shockwave radius
            Collider2D[] hits = Physics2D.OverlapCircleAll(origin, ShockwaveRadius);

            foreach (Collider2D hit in hits)
            {
                Player hitPlayer = hit.GetComponent<Player>();

                // Ignore the blocking player themselves
                if (hitPlayer == null || hitPlayer == ownerPlayer)
                {
                    continue;
                }

                // Push the hit player directly away from the blocker
                Vector2 direction = ((Vector2)hitPlayer.transform.position - origin).normalized;

                Rigidbody2D rb = hitPlayer.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(direction * KnockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
