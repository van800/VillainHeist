using UnityEngine;

namespace areas_and_respawn
{
    public abstract class Interactable : MonoBehaviour
    {
        protected Vector3 SavedPosition;
        protected Quaternion SavedRotation;
        protected bool SavedState;  // for toggleable object like lights, gates, or moving platforms
        
        public abstract void Interact();

        public abstract void Save();

        public abstract void Reset();
    }
}
