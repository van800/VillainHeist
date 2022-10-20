using System.Collections;
using System.Collections.Generic;
using movement_and_Camera_Scripts;
using UnityEngine;

public class InteractivityScript : MonoBehaviour
{
    public PlayerController player;
    
    public float interactDistance;
    
    [SerializeField]
    [Tooltip("Normal Material")]
    private Material regular;
    
    [SerializeField]
    [Tooltip("Interactivity Material / Can be picked up material")]
    private Material selectable;

    [SerializeField] private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if ( Vector3.Distance(player.transform.position, this.transform.position) < interactDistance)
        {
            rend.material = selectable;
        }
        else
        {
            rend.material = regular;
        }
    }
}
