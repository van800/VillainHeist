using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement_and_Camera_Scripts;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    public bool isInFirstPerson;
    public int totalBattery = -1;
    private PlayerController player;

    private GameState() { }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(changeFirstPerson());
        StartCoroutine(waitForBug());
    }

    public void setPlayer(PlayerController player)
    {
        this.player = player;
    }

    public void resetToTopDown()
    {
        isInFirstPerson = false;
        StopAllCoroutines();
        StartCoroutine(changeFirstPerson());
    }

    private IEnumerator changeFirstPerson()
    {
        yield return new WaitUntil(() => player.isFirstPov);
        isInFirstPerson = player.isFirstPov;
    }

    private IEnumerator waitForBug()
    {
        yield return new WaitUntil(() => isInFirstPerson);
        Debug.Log("Working!!!");
        while(true)
        {
            Debug.Log("isFP =  " + isInFirstPerson);
            yield return new WaitForEndOfFrame();
        }
    }

    public void updateTotalBat(int bat)
    {
        totalBattery = bat + player.maxBattery;
        player.AddMaxBattery(bat);
    }
}
