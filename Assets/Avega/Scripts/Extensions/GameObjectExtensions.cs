using UnityEngine;

namespace Avega.Extensions
{
    public static class GameObjectExtensions
    {
        public static bool TryGetComponentInAllObject<T>(this GameObject gameObject, out T component)
        {
            Transform root = gameObject.transform.root;
            
            if (root.TryGetComponent(out T component1))
            {
                component = component1;
                return true;
            }

            var componentInChildren = root.GetComponentInChildren<T>();
            component = componentInChildren;

            if (componentInChildren != null)
            {
                return true;
            }

            return false;
        }
    }
}