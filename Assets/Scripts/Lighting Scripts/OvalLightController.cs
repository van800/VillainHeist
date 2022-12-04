using areas_and_respawn;
using movement_and_Camera_Scripts;
using UnityEngine;

namespace Lighting_Scripts
{
    public class OvalLightController : Interactable
    {
        // [SerializeField] private bool selected;
        // [SerializeField] private Material highlightMaterial;
        // [SerializeField] private Material defaultMaterial;
        private Light _lightComponent;
        // private MeshRenderer _materialComponent;

        private bool _lightOn;

        [SerializeField] private GameObject[] hideWhenOff;
        
        [SerializeField] private GameObject[] hideWhenOn;

        private GuardController[] _guards;
        
        protected override void Initialize()
        {
            _lightComponent = GetComponent<Light>();
            _lightOn = false;

            _guards = GetComponentInParent<RoomController>().GetComponentsInParent<GuardController>();

            ToggleLight();
        }

        public override void Interact()
        {
            ToggleLight();
        }

        public override void Save()
        {
            SavedState = _lightOn;
            ShowHide();
        }

        public override void Reset()
        {
            _lightOn = !SavedState;
            ToggleLight();
        }
        

        // Toggle the associated LightSource
        public void ToggleLight()
        {
            _lightOn = !_lightOn;
            _lightComponent.enabled = _lightOn;

            ShowHide();
        }

        private void ShowHide()
        {
            if (_lightOn)
            {
                foreach (GameObject obj in hideWhenOn)
                {
                    obj.GetComponentInChildren<Renderer>().enabled = false;
                }
                foreach (GameObject obj in hideWhenOff)
                {
                    obj.GetComponentInChildren<Renderer>().enabled = true;
                }
                foreach (GuardController guard in _guards)
                {
                    guard.Unfreeze();
                }
            }
            else
            {
                foreach (GameObject obj in hideWhenOn)
                {
                    obj.GetComponentInChildren<Renderer>().enabled = true;
                }
                foreach (GameObject obj in hideWhenOff)
                {
                    print(obj.name);
                    obj.GetComponentInChildren<Renderer>().enabled = false;
                }
                foreach (GuardController guard in _guards)
                {
                    guard.Freeze();
                }
            }
        }
    }
}
