using System.Collections;
using System.Collections.Generic;
using movement_and_Camera_Scripts;
using UnityEngine;

public class GrabbableItem : MonoBehaviour
{
    public PlayerController player;

    [SerializeField]
    private bool pickedUp;

    public float pickUpDistance;

    [SerializeField] private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend.enabled = true;
    }

    void PickedUp()
    {
        this.transform.position = this.player.transform.position + new Vector3(0.0f, 10.0f, 0.0f);
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance(player.transform.position, this.transform.position) < this.pickUpDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
