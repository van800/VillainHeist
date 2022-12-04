using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneScript : MonoBehaviour
{
    private GameUI _gameUI;
    private Image _image;
    private IEnumerator DoAfterDelay(Action action, int delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    private bool fadingIn = false;
    private bool fadingOut = false;
    private float opacity = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        _gameUI = FindObjectOfType<GameUI>();
        
        _image = GetComponentInChildren<Image>();
        StartCoroutine(DoAfterDelay(() =>
        {
            _gameUI.ShowCutsceneText();
            StartCoroutine(DoAfterDelay(() =>
            {
                _gameUI.ShowCutsceneText();
                fadingIn = true;
                StartCoroutine(DoAfterDelay(() =>
                {
                    fadingIn = false;
                    fadingOut = true;
                    _gameUI.HideCutsceneText();
                    StartCoroutine(DoAfterDelay(() =>
                    {
                        SceneManager.LoadScene("MainMenu");

                    }, 5));
                }, 5));
            }, 3));
        }, 3));

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            opacity += Time.deltaTime / 2;
            _image.color = new Color(1, 1, 1, opacity);
        }

        if (fadingOut)
        {
            opacity -= Time.deltaTime / 2;
            _image.color = new Color(1, 1, 1, opacity);
        }
    }
}
