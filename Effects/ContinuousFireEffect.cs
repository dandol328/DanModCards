using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Continuously fires while the player holds the fire button.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        private Gun gun;

        private void Awake()
        {
            gun = GetComponent<Gun>();
        }

        private void Update()
        {
            if (gun == null || !Input.GetMouseButton(0))
                return;

            if (gun.IsReady())
                gun.Attack(0f);
        }
    }
}
