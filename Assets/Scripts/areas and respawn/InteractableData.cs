using UnityEngine;

namespace areas_and_respawn
{
    public class InteractableData : MonoBehaviour
    {
        private Vector3 _savedPosition;
        private Quaternion _savedRotation;
        public bool currentState;  // for toggleable object like lights, gates, or moving platforms
        private bool _savedState;  // currentState should be updated from other scripts
        
        // Start is called before the first frame update
        void Start()
        {
            Save();
        }

        public void Save()
        {
            Transform t = transform;
            _savedPosition = t.position;
            _savedRotation = t.rotation;
            _savedState = currentState;
        }

        public void Reset()
        {
            Transform t = transform;
            t.position = _savedPosition;
            t.rotation = _savedRotation;
            //TODO: figure out how to reset toggleable interactables
        }
    }
}
