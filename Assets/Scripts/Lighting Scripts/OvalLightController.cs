using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvalLightController : MonoBehaviour
{
    private Transform _lightSource;
    
    // Start is called before the first frame update
    void Start()
    {
        Transform lightSource = transform.Find("LightSource");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Toggle the associated LightSource
    private void ToggleLight()
    {
        _lightSource.BroadcastMessage("Toggle");
    }
}
