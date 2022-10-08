using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This will apply a given ApplyUnapplyEverySecsFunc or ApplyEverySecsFunc
// every given number of seconds.
public class ApplyEverySecs : MonoBehaviour
{
    [SerializeField]
    private float seconds = 0;
    private ApplyEverySecsFunc func;
    [SerializeField]
    private bool startOn = true;
    private bool isOn;
    private bool isOff;
    // Start is called before the first frame update
    void Start()
    {
        SetActive(startOn);
    }

    // Applies and unapplies a method every given seconds sec
    private IEnumerator applyEverySeconds()
    {
        while (isOn)
        {
            func.Apply();
            yield return new WaitUntil(() => func.FinishedApplying());
            yield return new WaitForSeconds(seconds);
        }
        if (!isOff)
        {
            TurnOff();
        }
    }

    // Returns the state before running apply or the state after running unapply
    public void TurnOff()
    {
        isOn = false;
        isOff = true;
        func.TurnOff();
    }

    // Restarts this script
    public void TurnOn()
    {
        isOff = false;
        isOn = true;
        func = GetComponent<ApplyEverySecsFunc>();
        StartCoroutine(applyEverySeconds());
    }

    // Sets if this script is running or not
    // If this script is no longer running, this will return to the unapply state or the state before apply
    public void SetActive(bool active)
    {
        if (active && !isOn)
        {
            TurnOn();
        }
        else if (!isOff)
        {
            TurnOff();
        }
    }
}

// A class that will apply and unapply itself every couple of seconds
public abstract class ApplyUnapplyEverySecsFunc : ApplyEverySecsFunc
{
    private float seconds;
    //private bool applyIsDone = false;
    //private bool unapplyIsDone = false;
    private bool runApply;
    // If true, run Apply
    // If false, run Unapply

    private void Start()
    {
        runApply = true;
    }

    public void SetSeconds(float seconds)
    {
        this.seconds = seconds;
    }

    public float GetSeconds()
    {
        return seconds;
    }

    sealed public override void Apply()
    {
        if (runApply)
        {
            StartApply();
        }
        else
        {
            StartUnapply();
        }
    }

    public abstract void StartUnapply();
}

// A class that will apply itself every couple of seconds with a ApplyEverySecs
public abstract class ApplyEverySecsFunc : MonoBehaviour
{
    private float seconds;
    private bool applyIsDone = false;
    public void SetSeconds(float seconds)
    {
        this.seconds = seconds;
    }

    public float GetSeconds()
    {
        return seconds;
    }

    public virtual void Apply()
    {
        StartApply();
    }

    // This is here to make the ApplyEverySecsFunc and ApplyUnapplyEverySecsFunc
    // use the same apply method name.
    public abstract void StartApply();

    public abstract void TurnOff();

    // Returns true when apply is finished and resets apply
    public bool FinishedApplying()
    {
        if (applyIsDone)
        {
            applyIsDone = false;
            return true;
        }
        return false;
    }

    // Sets apply to be done for this frame
    protected void SetApplyDone()
    {
        applyIsDone = true;
    }
}