using System;
using System.ComponentModel;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class AreaController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Top Down Camera Boundary")]
        public Collider boundary;

        private ComponentCollection _components;

        public void Reset()
        {
            throw new NotImplementedException();
            //TODO: add room reset
        }
    }
}
