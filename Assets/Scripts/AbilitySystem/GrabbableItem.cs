using System.Collections;
using System.Collections.Generic;
using movement_and_Camera_Scripts;
using UnityEngine;
using Unity.Mathematics;

public class GrabbableItem : MonoBehaviour
{
    public PlayerController player;

    [SerializeField]
    private bool isPickedUp;

    public float pickUpDistance;

    private float originalY;

    [SerializeField]
    [Tooltip("Normal Material")]
    private Material regular;
    
    [SerializeField]
    [Tooltip("Interactivity Material / Can be picked up material")]
    private Material selectable;

    public float playerYRot;

    [SerializeField] private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend.enabled = true;
        rend.material = regular;
        isPickedUp = false;
        this.originalY = this.transform.position.y;
    }

    void PickedUp()
    {
        this.transform.position = this.player.transform.position + new Vector3(0.0f, 20.0f, 0.0f);
        rend.enabled = false;
        this.isPickedUp = true;
    }

    void PutDown()
    {
        float rot = player.transform.rotation.eulerAngles.y * math.PI / 180;
        rend.enabled = true;
        this.transform.position = this.player.transform.position +
                                  new Vector3(math.sin((rot)) * (pickUpDistance + .05f),
                                      -player.transform.position.y + originalY,
                                      math.cos(rot) * (pickUpDistance + .05f));
        this.isPickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y") && isPickedUp)
        {
            this.PutDown();
        }

        playerYRot = player.transform.rotation.eulerAngles.y;

        if ( Vector3.Distance(player.transform.position, this.transform.position) < this.pickUpDistance
             && !isPickedUp)
        {
            rend.material = selectable;
            if (Input.GetKeyDown("t"))
            {
                this.PickedUp();
            }
        }
        else
        {
            rend.material = regular;
        }
    }
}
