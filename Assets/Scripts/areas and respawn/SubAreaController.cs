using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace areas_and_respawn
{
    public class SubAreaController : MonoBehaviour
    {
        private SubAreaController[] _subAreas;

        private readonly List<Interactable> _intractables = new();
        
        private void Start()
        {
            _subAreas = GetComponentsInChildren<SubAreaController>();
            foreach (Transform t in transform)
            {
                if (t.CompareTag($"Interactable"))
                {
                    _intractables.Add(t.gameObject.GetComponent<Interactable>());
                }
                else if (t.gameObject.TryGetComponent(out SubAreaController subArea))
                {
                    _intractables.AddRange(subArea._intractables);
                }
            }
        }

        public void Save()
        {
            foreach (Interactable i in _intractables) i.Save();
        }

        public void Reset()
        {
            foreach (Interactable i in _intractables) i.Reset();
            for (int i = 1; i < _subAreas.Length; i++) // call on sub areas, exclude own area
            {
                _subAreas[i].Reset();
            }
        }
    }
}
