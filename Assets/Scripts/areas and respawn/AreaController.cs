using System;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

namespace areas_and_respawn
{
    public class AreaController : MonoBehaviour
    {
        private BoxCollider _boundary;

        private BoxCollider _cameraBoundary;
        
        private SubAreaController[] _subAreas;

        public CheckPointController spawnPoint;
        
        private void Start()
        {
            _subAreas = GetComponentsInChildren<SubAreaController>();

            _boundary = GetComponent<BoxCollider>();
            _boundary.isTrigger = true;
        }

        public void Reset()
        {
           foreach (SubAreaController subArea in _subAreas) // call on sub areas
           {
               subArea.Reset();
           }
        }

        public void Save()
        {
            foreach (SubAreaController subArea in _subAreas) // call on sub areas
            {
                subArea.Save();
            }
        }

        public BoxCollider GetCameraBoundary()
        {
            _boundary = GetComponent<BoxCollider>();
            _boundary.isTrigger = true;
            
            if (_cameraBoundary is null)
            {
                _cameraBoundary = gameObject.AddComponent<BoxCollider>();
                _cameraBoundary.center = _boundary.center - new Vector3(0, 0, 5);
                _cameraBoundary.size = _boundary.size;
                _cameraBoundary.isTrigger = true;
            }
            
            return _cameraBoundary;
        }
    }
}
