using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyEverySecs : MonoBehaviour
{
    [SerializeField]
    private float seconds = 0;
    private ApplyEverySecsFunc func;
    [SerializeField]
    private bool on = true;
    // Start is called before the first frame update
    void Start()
    {
        func = GetComponent<ApplyEverySecsFunc>();
        StartCoroutine(applyEverySeconds());
    }

    // Applies and unapplies a method every given seconds sec
    private IEnumerator applyEverySeconds()
    {
        while (on)
        {
            // Starts by running the apply method
            func.Apply();
            yield return new WaitUntil(() => func.ShouldStartUnapply() );
            yield return new WaitForSeconds(seconds);
            // then runs the unapply method
            func.Unapply();
            yield return new WaitUntil(() => func.ShouldStartApply());
            yield return new WaitForSeconds(seconds);
        }
        OnTurnOff();
    }

    // Returns the state before running apply or the state after running unapply
    public void OnTurnOff()
    {
        func.OnTurnOff();
    }

    // Sets if this script is running or not
    // If this script is no longer running, this will return to the unapply state or the state before apply
    public void SetActive(bool active)
    {
        on = active;
    }
}

public abstract class ApplyEverySecsFunc : MonoBehaviour
{
    private float seconds;
    private bool applyIsDone = false;
    private bool unapplyIsDone = false;
    public void SetSeconds(float seconds)
    {
        this.seconds = seconds;
    }

    public float GetSeconds()
    {
        return seconds;
    }

    public abstract void Apply();

    public abstract void Unapply();

    public abstract void OnTurnOff();

    // Returns true when apply is finished and resets apply
    public bool ShouldStartUnapply()
    {
        if (applyIsDone)
        {
            applyIsDone = false;
            return true;
        }
        return false;
    }

    // Returns true when unapply is finished and resets unapply
    public bool ShouldStartApply()
    {
        if (unapplyIsDone)
        {
            unapplyIsDone = false;
            return true;
        }
        return false;
    }

    // Sets apply to be done for this frame
    protected void SetApplyDone()
    {
        applyIsDone = true;
    }

    // Sets unapply to be done for this frame
    protected void SetUnapplyDone()
    {
        unapplyIsDone = true;
    }
}