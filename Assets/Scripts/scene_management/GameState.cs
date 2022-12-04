using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement_and_Camera_Scripts;

public class GameState : MonoBehaviour
{
    public static GameState Instance { get; private set; }
    public bool isInFirstPerson;
    private PlayerController player;
    private static GameObject gameObj;

    private GameState() { }

    private void Awake()
    {
        if (gameObj == null)
        {
            gameObj = gameObject;
            Instance = this;
            DontDestroyOnLoad(gameObj);
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
    }

    private IEnumerator changeFirstPerson()
    {
        yield return new WaitUntil(() => player.isFirstPov);
        isInFirstPerson = player.isFirstPov;
    }
}
