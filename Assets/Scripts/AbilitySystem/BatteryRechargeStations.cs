using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryRechargeStations : MonoBehaviour
{

    [Tooltip("Insert Battery Storage Object")]
    public Battery bat;

    [Tooltip("The Player")]
    public GameObject thePlayer;

    [SerializeField] [Tooltip("Distance Until the item is picked up")]
    private float chargeDistance;
    

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance(thePlayer.transform.position, this.transform.position) < this.chargeDistance)
        {
            this.bat.refill();
        }
    }
}
