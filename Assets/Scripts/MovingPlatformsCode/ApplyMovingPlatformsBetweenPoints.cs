using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMovingPlatformsBetweenPoints : ApplyEverySecsFunc
{
    [Header("Movement Prep")]
    [SerializeField]
    [Tooltip("Insert the empty gameobject that stores the end position of this moving platform here")]
    private Transform[] goToTransform;
    private int nextPos;
    private Vector3[] endPos;
    private Vector3 startPos;
    private static readonly string[] movableTags = { "Untagged"};

    [Header("Current Movement")]
    [SerializeField]
    private float speed = .5f;
    private bool running;
    private Vector3 targetLoc;
    private Vector3 prevTargetLoc;

    private void Start()
    {
        nextPos = 0;
        endPos = new Vector3[goToTransform.Length];
        for (int i = 0; i < goToTransform.Length; i++)
        {
            endPos[i] = goToTransform[i].position;
        }
        startPos = transform.position;
    }

    private void Update()
    {
        if (running)
        {
            Move();
            CheckEnd();
        }
    }

    public override void StartApply()
    {
        running = true;
        prevTargetLoc = transform.position;
        if (nextPos < endPos.Length)
        {
            targetLoc = endPos[nextPos];
            nextPos++;
        }
        else
        {
            targetLoc = startPos;
            nextPos = 0;
        }
    }

    public override void TurnOff()
    {
        //Do nothing and have the platform stop at the next point
    }

    /*public override void Unapply()
    {
        running = true;
        targetLoc = startPos;
    }*/

    // Moves this platform to the current target pos
    private void Move()
    {
        transform.Translate((targetLoc - transform.position).normalized * speed * Time.deltaTime);
    }

    // Ends the current method (whether Apply or UnApply) if the platform has reached its destination
    // This means that the distance between its current location and its target is 0
    private void CheckEnd()
    {
        if (AtTarget())
        {
            running = false;
            // This means we have reached our target location
            /*if (targetLoc.Equals(endPos))
            {
                Debug.Log("Apply is Done");
                // This means we are running apply
                SetApplyDone();
            }
            else if (targetLoc.Equals(startPos))
            {
                Debug.Log("Unapply is Done");
                // This means we are running unapply
                SetUnapplyDone();
            }*/
            SetApplyDone();
        }
    }

    // Returns true if this platform made it to its target and false otherwise
    private bool AtTarget()
    {
        //return ((targetLoc - transform.position).magnitude < .1f);
        //Vector3 origPos = prevTargetLoc;
        // If this platform is farther from the non-target position than the
        // target location is, then it has passed the target
        return ((transform.position - prevTargetLoc).magnitude > (targetLoc - prevTargetLoc).magnitude);
    }

    // Runs when an object is placed on this
    private void OnTriggerEnter(Collider other)
    {
        // Adds all objects with certain tags on top of this object as a child
        // This way, all objects on top of this one will move with this object
        foreach (string tag in movableTags)
        {
            if (other.CompareTag(tag))
            {
                other.transform.SetParent(transform);
            }
        }
    }

    // Runs when an object is removed from this
    private void OnTriggerExit(Collider other)
    {
        // Removes all objects that were added as children when they leave this platform
        foreach (string tag in movableTags)
        {
            // The object on this one only stops being a child of this object if it is
            // still a child of this object and has not been snatched by another object
            // and had its parent changed.
            if (other.CompareTag(tag) && other.transform.parent.Equals(transform))
            {
                other.transform.SetParent(null);
            }
        }
    }
}
