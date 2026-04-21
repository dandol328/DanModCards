using System.Collections;
using UnityEngine;

namespace DanModCards.Effects
{
    /// <summary>
    /// Continuously fires while the player holds the fire button.
    /// Fires on a short loop instead of every frame for better control.
    /// </summary>
    public class ContinuousFireEffect : MonoBehaviour
    {
        private const float FireInterval = 0.01f;

        private Gun gun;
        private Coroutine fireRoutine;

        private void Start()
        {
            gun = GetComponent<Gun>();
        }

        private void Update()
        {
            if (gun == null) return;

            if (Input.GetMouseButton(0))
            {
                if (fireRoutine == null)
                {
                    fireRoutine = StartCoroutine(FireLoop());
                }
            }
            else if (fireRoutine != null)
            {
                StopCoroutine(fireRoutine);
                fireRoutine = null;
            }
        }

        private IEnumerator FireLoop()
        {
            while (Input.GetMouseButton(0))
            {
                gun.Attack(0f, false);
                yield return new WaitForSeconds(FireInterval);
            }

            fireRoutine = null;
        }

        private void OnDisable()
        {
            if (fireRoutine != null)
            {
                StopCoroutine(fireRoutine);
                fireRoutine = null;
            }
        }

        private void OnDestroy()
        {
            if (fireRoutine != null)
            {
                StopCoroutine(fireRoutine);
                fireRoutine = null;
            }
        }
    }
}
