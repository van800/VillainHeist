using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalLightController : MonoBehaviour
{
    [SerializeField] private bool selected;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defaultMaterial;
    private Light _lightComponent;
    private MeshRenderer _materialComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        _lightComponent = this.GetComponent<Light>();
        _materialComponent = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            _materialComponent.material = highlightMaterial;
            if (Input.GetKeyUp("e"))
            {
                ToggleLight();
            }
        }
        else
        {
            _materialComponent.material = defaultMaterial;
        }
        
        

    }

    // Toggle the associated LightSource
    private void ToggleLight()
    {
        _lightComponent.enabled = !_lightComponent.enabled;
    }

    void deselect()
    {
        selected = false;
    }

    void select()
    {
        selected = true;
    }
}
