using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
    private Light _lightComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        _lightComponent = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Toggle()
    {
        _lightComponent.enabled = !_lightComponent.enabled;
    }
}
