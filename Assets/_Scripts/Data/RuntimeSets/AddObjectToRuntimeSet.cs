using UnityEngine;

namespace _Scripts.Data.RuntimeSets
{
    public class AddObjectToRuntimeSet : MonoBehaviour
    {
        public GameObjectRuntimeSet GameObjectRuntimeSet;

        private void OnEnable()
        {
            GameObjectRuntimeSet.AddToList(gameObject);
        }

        private void OnDisable()
        {
            GameObjectRuntimeSet.RemoveFromList(gameObject);
        }
    }
}