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
            //TODO: add room reset
            /*
             * subarea keeps track of the objects that need to be saved/reset
             * 
             */
        }
    }
}
