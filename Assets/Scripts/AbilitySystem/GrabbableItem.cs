using System.Collections;
using System.Collections.Generic;
using areas_and_respawn;
using movement_and_Camera_Scripts;
using UnityEngine;
using Unity.Mathematics;

public class GrabbableItem : MonoBehaviour
{
    private PlayerController _player;

    [SerializeField]
    private bool isPickedUp;
    
    private float originalY;

    [SerializeField]
    [Tooltip("Normal Material")]
    private Material regular;
    
    [SerializeField]
    [Tooltip("Interactivity Material / Can be picked up material")]
    private Material selectable;

    public float playerYRot;

    [SerializeField] private Renderer rend;

    private InteractableData _data;
    
    // Start is called before the first frame update
    void Start()
    {
        rend.enabled = true;
        rend.material = regular;
        isPickedUp = false;
        this.originalY = this.transform.position.y;
        _player = FindObjectOfType<PlayerController>();
        _data = gameObject.AddComponent<InteractableData>();
    }

    void PickedUp()
    {
        this.transform.position = this._player.transform.position + new Vector3(0.0f, 20.0f, 0.0f);
        rend.enabled = false;
        this.isPickedUp = true;
        _data.SetState(true);  // set the stored state
    }

    void PutDown()
    {
        float pickUpDistance = _player.interactDistance;
        float rot = _player.transform.rotation.eulerAngles.y * math.PI / 180;
        rend.enabled = true;
        this.transform.position = this._player.transform.position +
                                  new Vector3(math.sin((rot)) * (pickUpDistance + .05f),
                                      -_player.transform.position.y + originalY,
                                      math.cos(rot) * (pickUpDistance + .05f));
        this.isPickedUp = false;
        _data.SetState(false);
    }

    // Update is called once per frame
    void Update()
    {
        isPickedUp = _data.GetState();
        if (Input.GetKeyDown("y") && isPickedUp)
        {
            this.PutDown();
        }

        playerYRot = _player.transform.rotation.eulerAngles.y;

        if ( Vector3.Distance(_player.transform.position, this.transform.position) < _player.interactDistance
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
