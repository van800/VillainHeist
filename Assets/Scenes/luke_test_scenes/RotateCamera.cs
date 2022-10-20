using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateCamera : MonoBehaviour
{

    public float speed = 100;
    private int angle = 225;
    private Vector3 targetPos = new Vector3(14, 14, 14);

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            angle += 90;
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            angle += 270;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(32, angle, 0), Time.deltaTime * speed * 90);

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed * 28);
        
        switch (angle % 360)
        {
            case 315:
                targetPos = new Vector3(14, 14, -14);
                break;
            case 225:
                targetPos = new Vector3(14, 14, 14);
                break;
            case 135:
                targetPos = new Vector3(-14, 14, 14);
                break;
            case 45:
                targetPos = new Vector3(-14, 14, -14);
                break;
        }
        
    }
}
