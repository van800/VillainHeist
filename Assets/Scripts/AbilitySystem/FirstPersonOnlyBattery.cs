using System.Collections;
using System.Collections.Generic;
using AbilitySystem;
using movement_and_Camera_Scripts;
using UnityEngine;

public class FirstPersonOnlyBattery : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private bool active;
    private Renderer rend;
    [SerializeField]
    private BatteryPickupItem mainScript;
    private float realDistance;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rend = GetComponent<Renderer>();
        mainScript = GetComponent<BatteryPickupItem>();
        realDistance = mainScript.killDistance;
        mainScript.killDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            active = player.GetComponent<PlayerController>().isFirstPov;
            rend.enabled = active;
            if (active)
            {
                mainScript.killDistance = realDistance;
            }
        }
    } 
}

