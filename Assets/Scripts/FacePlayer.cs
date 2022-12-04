using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement_and_Camera_Scripts;

public class FacePlayer : MonoBehaviour
{
    private PlayerController player;
    private Quaternion oldRotation;
    // Start is called before the first frame update
    void Start()
    {
        oldRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isFirstPov)
        {
            updateRotation();
        }
        else
        {
            transform.rotation = oldRotation;
        }
    }

    private void updateRotation()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
