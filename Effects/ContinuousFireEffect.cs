using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Enables continuous fire while the player holds the fire button.
    /// Attach to the gun's GameObject.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        private Gun    gun;
        private Player player;

        private void Start()
        {
            gun    = GetComponent<Gun>();
            player = GetComponentInParent<Player>();
        }

        private void Update()
        {
            if (gun == null || player == null) return;

            if (player.data.playerActions.Attack.IsPressed)
            {
                gun.Attack(0f, false);
            }
        }
    }
}
