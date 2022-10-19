using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickupItem : MonoBehaviour
{

    [Tooltip("Insert Battery Storage Object")]
    public Battery bat;

    [SerializeField]
    [Tooltip("Add Box Collider")]
    public bool colliding = false;

    public GameObject killGuy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        this.bat.addToCurrent(2);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance(killGuy.transform.position, this.transform.position) < 1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
