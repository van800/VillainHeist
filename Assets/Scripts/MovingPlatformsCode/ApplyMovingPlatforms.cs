using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyMovingPlatforms : ApplyEverySecsFunc
{
    [Header("Movement Prep")]
    [SerializeField]
    [Tooltip("Insert the empty gameobject that stores the end position of this moving platform here")]
    private Transform goToTransform;
    private Vector3 endPos;
    private Vector3 startPos;
    private static readonly string[] movableTags = { "Untagged"};

    [Header("Current Movement")]
    [SerializeField]
    private float speed = .5f;
    private bool running;
    private Vector3 targetLoc;

    private void Start()
    {
        endPos = goToTransform.position;
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

    public override void Apply()
    {
        running = true;
        targetLoc = endPos;
    }

    public override void OnTurnOff()
    {
        Unapply();
    }

    public override void Unapply()
    {
        running = true;
        targetLoc = startPos;
    }

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
            if (targetLoc.Equals(endPos))
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
            }
        }
    }

    // Returns true if this platform made it to its target and false otherwise
    private bool AtTarget()
    {
        //return ((targetLoc - transform.position).magnitude < .1f);
        Vector3 origPos = startPos + endPos - targetLoc;
        // If this platform is farther from the non-target position than the
        // target location is, then it has passed the target
        return ((transform.position - origPos).magnitude > (targetLoc - origPos).magnitude);
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
            if (other.CompareTag(tag))
            {
                other.transform.SetParent(null);
            }
        }
    }
}
