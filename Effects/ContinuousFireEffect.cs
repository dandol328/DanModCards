using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Enables continuous fire while the player holds the fire button.
    /// Attach to the gun's GameObject.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        private Gun gun;

        private void Start()
        {
            gun = GetComponent<Gun>();
        }

        private void Update()
        {
            if (gun == null) return;

            if (Input.GetMouseButton(0))
            {
                gun.Attack(0f, false);
            }
        }
    }
}
