using System.Collections;
using System.Collections.Generic;
using movement_and_Camera_Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class KillLaser : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private bool canKill;
    public GameObject killed;
    [SerializeField]
    private bool killMaybe;
    private GameObject target;
    [SerializeField]
    private GameObject EffectsYeah;

    [SerializeField] private GameObject buddy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        buddy = GameObject.Find("1st Person");
        buddy = GameObject.Find("Eyeline");
    }

    // Update is called once per frame
    void Update()
    {
        canKill = player.GetComponent<PlayerController>().isFirstPov;
        
        if (Input.GetMouseButtonDown(0) && canKill)
        {
            
            var ray = new Ray(buddy.transform.position,
                player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //killMaybe = true;
                target = hit.transform.gameObject;
                killed = target;
                if (target.TryGetComponent(out GuardController bingle))
                {
                    GameObject bob = Instantiate(EffectsYeah);
                    bob.transform.position = target.transform.position;
                    Destroy(target);
                    Destroy(bob, 2);
                }
            }
        }
    }
}
