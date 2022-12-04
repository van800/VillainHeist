using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using movement_and_Camera_Scripts;

public class EscapeTimer : MonoBehaviour
{
    public static EscapeTimer Instance;
    private static bool exists = false;
    private PlayerController player;
    public float totalSecondsRemaining;
    [SerializeField] private float totalTime = 60;
    private bool useTimer = false;
    [Header("UI")]
    [SerializeField] private UIDocument timerUi;
    [SerializeField] private string timerName = "timer";
    private Label timerText;
    [SerializeField] private float timerBlinkTime = .1f;

    /*private void Awake()
    {
        if (exists)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            exists = true;
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        if (exists)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            exists = true;
        }
        totalSecondsRemaining = totalTime;
        //StartCoroutine(waitUntilEscape());
        VisualElement root = timerUi.rootVisualElement;
        timerText = root.Q<Label>(timerName);
        showTimer(false);
    }

    /*private IEnumerator waitUntilEscape()
    {
        this.player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        Debug.Log("begin start timer");
        yield return new WaitUntil(() => !player.isFirstPov);
        Debug.Log("begin start timer 2");
        yield return new WaitUntil(() => player.isFirstPov);
        Debug.Log("start timer");
        useTimer = true;
        showTimer(true);
        StartCoroutine(timerUI());
    }*/

    public void startTimer()
    {
        useTimer = true;
        showTimer(true);
        StartCoroutine(timerUI());
    }

    private void reset()
    {
        this.player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();

        //player.ToPov(false);
        GameState.Instance.resetToTopDown();

        SceneManager.LoadScene("FinalRoom");
        useTimer = false;
        Debug.Log("reset timer");
        showTimer(false);
        StopAllCoroutines();
        totalSecondsRemaining = totalTime;
        //StartCoroutine(waitUntilEscape());
    }

    private void showTimer(bool show)
    {
        if (show)
        {
            timerText.style.display = DisplayStyle.Flex;
        }
        else
        {
            timerText.style.display = DisplayStyle.None;
        }
    }

    private bool isShowingTimer()
    {
        return timerText.style.display == DisplayStyle.Flex;
    }

    // Update is called once per frame
    void Update()
    {
        if (useTimer)
        {
            updateTimer();
            updateTimerText();
            checkEnd();
        }
    }

    private void updateTimer()
    {
        totalSecondsRemaining -= Time.deltaTime;
    }

    private IEnumerator timerUI()
    {
        while (true)
        {
            yield return new WaitForSeconds(timerBlinkTime);
            showTimer(!isShowingTimer());
        }
    }

    private void updateTimerText()
    {
        int min = getMinutesRemaining();
        int sec = getSecondsRemaining();
        timerText.text = min + ":";
        if (sec >= 10)
        {
            timerText.text += sec;
        }
        else
        {
            timerText.text += "0" + sec;
        }
        
        // Formats the timer as min:sec (where sec must have two digits)
        // Ex. 0:00, 1:04, 3:35
    }

    public int getMinutesRemaining()
    {
        return (Mathf.RoundToInt(totalSecondsRemaining) / 60);
    }

    public int getSecondsRemaining()
    {
        return (Mathf.RoundToInt(totalSecondsRemaining) % 60);
    }

    public int getTotalSecondsRemaining()
    {
        return Mathf.RoundToInt(totalSecondsRemaining);
    }

    private void checkEnd()
    {
        if (totalSecondsRemaining <= 0)
        {
            // Failure Script
            //Time.timeScale = 0;
            // To be implemented
            reset();
        }
    }
}
