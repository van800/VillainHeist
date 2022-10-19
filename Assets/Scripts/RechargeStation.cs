using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeStation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Add AbilityInputSystem")]
    public AbilityInputs abilityInputSystem;
    
    [SerializeField]
    [Tooltip("Add Box Collider")]
    public bool colliding = false;
    
    public GameObject killGuy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Vector3.Distance(killGuy.transform.position, this.transform.position) < 1.0f)
        {
            abilityInputSystem.refill();
        }
    }
}
