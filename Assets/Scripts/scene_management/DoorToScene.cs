using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using movement_and_Camera_Scripts;

public class DoorToScene : MonoBehaviour
{
    public string nextScene;
    private PlayerController player;
    [SerializeField] private bool onlyInTopDown;
    [SerializeField] private Text distanceText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isFirstPov)
        {
            updateDistanceText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player.isFirstPov == !onlyInTopDown)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    private void updateDistanceText()
    {
        if (distanceText != null)
        {
            distanceText.text = Mathf.RoundToInt((player.transform.position - transform.position).magnitude) + "M";
        }
    }
}
