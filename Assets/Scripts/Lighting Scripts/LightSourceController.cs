using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceController : MonoBehaviour
{
    private Light lightComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        lightComponent = this.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Toggle()
    {
        lightComponent.enabled = !lightComponent.enabled;
    }
}
