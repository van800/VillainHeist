using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument _uiDocument;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _uiDocument.rootVisualElement.Q<Button>("Play").clicked += () => Play();
        _uiDocument.rootVisualElement.Q<Button>("Credits").clicked += () => Credits();
        _uiDocument.rootVisualElement.Q<Button>("AssetShowcase").clicked += () => AssetShowcase();
        _uiDocument.rootVisualElement.Q<Button>("Quit").clicked += () => Quit();
    }

    private void Play()
    {
        SceneManager.LoadScene("Tutorial");
        print("PLAY");
    }
    
    private void Credits()
    {
        print("CREDITS");
    }
    
    private void AssetShowcase()
    {
        SceneManager.LoadScene("AssetShowcase");
        print("SHOWCASE");
    }

    private void Quit()
    {
        print("QUIT");
        Application.Quit();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
