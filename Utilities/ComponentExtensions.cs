using UnityEngine;

namespace DanModCards.Utilities
{
    public static class ComponentExtensions
    {
        /// <summary>
        /// Gets a component of type T from the GameObject, or adds one if it doesn't exist.
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T component = go.GetComponent<T>();
            if (!component)
            {
                component = go.AddComponent<T>();
            }
            return component;
        }
    }
}
