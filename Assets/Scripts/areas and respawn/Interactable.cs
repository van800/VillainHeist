using System;
using movement_and_Camera_Scripts;
using UnityEngine;

namespace areas_and_respawn
{
    public abstract class Interactable : MonoBehaviour
    {
        protected Vector3 SavedPosition;
        protected Quaternion SavedRotation;
        protected bool SavedState;  // for toggleable object like lights, gates, or moving platforms

        protected Renderer Renderer;
        [SerializeField] [Tooltip("Default/Starting Material")]
        protected Material regular;
        [SerializeField] [Tooltip("Interactivity Material / Can be picked up material")]
        private Material selectedMaterial;

        private PlayerController _player;

        protected void Awake()
        {
            Renderer = GetComponent<MeshRenderer>();
            Renderer.enabled = true;
            if (regular is not null)
            {
                regular = Renderer.material;
            }
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        public abstract void Interact();

        public abstract void Save();

        public abstract void Reset();

        public virtual void InRange()
        {
            Renderer.material = selectedMaterial;
            Invoke(nameof(OutOfRange), 1f);
        }

        protected virtual void OutOfRange()
        {
            Transform playerTransform = _player.transform;
            Physics.Raycast(playerTransform.position + Vector3.up / 2, playerTransform.forward,
                out RaycastHit hit, _player.interactDistance);
            if (hit.transform != transform)
            {
                Renderer.material = regular;
            }
        }
    }
}
