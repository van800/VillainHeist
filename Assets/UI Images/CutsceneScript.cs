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
    private AudioSource _cutsceneAS1;
    private IEnumerator DoAfterDelay(Action action, int delay)
    {
        yield return new WaitForSeconds(delay);
        action.Invoke();
    }

    private bool fadingIn = false;
    private bool fadingOut = false;
    private float opacity = 0;
    private float volume = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _cutsceneAS1 = FindObjectOfType<AudioSource>();
        _gameUI = FindObjectOfType<GameUI>();
        
        _image = GetComponentInChildren<Image>();
        StartCoroutine(DoAfterDelay(() =>
        {
            _gameUI.ShowCutsceneText();
            StartCoroutine(DoAfterDelay(() =>
            {
                _gameUI.ShowCutsceneText();
                fadingIn = true;
                _cutsceneAS1.Play();
                StartCoroutine(DoAfterDelay(() =>
                {
                    fadingIn = false;
                    fadingOut = true;
                    _gameUI.HideCutsceneText();
                    StartCoroutine(DoAfterDelay(() =>
                    {
                        SceneManager.LoadScene("MainMenu");

                    }, 5));
                }, 15));
            }, 3));
        }, 3));

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            opacity = Math.Min(opacity + (Time.deltaTime / 2), 1);
            _image.color = new Color(1, 1, 1, opacity);
            volume = Math.Min(volume + Time.deltaTime, 1);
            _cutsceneAS1.volume = volume;
        }

        if (fadingOut)
        {
            opacity = Math.Max(opacity - (Time.deltaTime / 2), 0);
            _image.color = new Color(1, 1, 1, opacity);
            volume = Math.Max(volume - Time.deltaTime, 0);
            _cutsceneAS1.volume = volume;
        }
    }
}
