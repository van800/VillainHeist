using System;
using System.Collections;
using System.Collections.Generic;
using areas_and_respawn;
using movement_and_Camera_Scripts;
using UnityEngine;
using UnityEngine.UIElements;

public class MarkerScript : MonoBehaviour
{
    public bool canFlip;
    public GameObject player;
    private PlayerController _playerController;
    private bool hasPickedUp;
    
    [SerializeField]
    private float pickUpDistance;

    public string keyActivator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _playerController = player.GetComponent<PlayerController>();
        if (GameState.Instance.isInFirstPerson)
        {
            hasPickedUp = true;
            GetComponent<Renderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position + Vector3.down, player.transform.position) < pickUpDistance)
        {
            canFlip = true;
            GetComponent<Renderer>().enabled = false;
            if (!hasPickedUp)
            {
                _playerController.PlayPickupSound();
                hasPickedUp = true;
            }
        }
    }

    /*
    protected override void Initialize()
    {
        player = GameObject.FindWithTag("Player");
    }

    public override void Interact()
    {
        // call move all connected walls
    }

    public override void Save()
    {
        // SavedState = currentState
        //currentstate is the bool that hold walls being up/down
    }

    public override void Reset()
    {
        //currentState = SavedState
    } */
}
