using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentBat;

    [SerializeField]
    [Tooltip("Maximum Battery Amount")]
    public int maxBat;

    public Battery(int max)
    {
        this.currentBat = max;
        this.maxBat = max;
    }

    public void refill()
    {
        this.currentBat = this.maxBat;
    }

    public void addToCurrent(int amount)
    {
        this.currentBat += amount;
        if (this.currentBat > this.maxBat)
        {
            this.currentBat = this.maxBat;
        }
    }

    public void subFromCurrent(int amount)
    {
        this.currentBat -= amount;
    }
    public bool hasLeft(int amount)
    {
        return this.currentBat >= amount;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
