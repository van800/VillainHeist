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
        var button = new Button() { text = "保存数据" };
        //button.AddToClassList("horizontalContainer");
        
        if (GameState.Instance && GameState.Instance.gameObject)
        {
            Destroy(GameState.Instance.gameObject);
        }
        if (EscapeTimer.Instance && EscapeTimer.Instance.gameObject)
        {
            Destroy(EscapeTimer.Instance.gameObject);
        }
        
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        _uiDocument = GetComponent<UIDocument>();
        // _uiDocument.rootVisualElement.Q<Label>("TestLabel").text = _uiDocument.rootVisualElement.Q<Label>("TestLabel").text+ "testssss";
        _uiDocument.rootVisualElement.Q<Label>("TestLabel").AddToClassList("testClass");
        _uiDocument.rootVisualElement.Q<Button>("Play").clicked += () => Play();
        _uiDocument.rootVisualElement.Q<Button>("Credits").clicked += () => Credits();
        _uiDocument.rootVisualElement.Q<Button>("AssetShowcase").clicked += () => AssetShowcase();
        _uiDocument.rootVisualElement.Q<Button>("Quit").clicked += () => Quit();
        _uiDocument.rootVisualElement.Q<Button>("Done").clicked += () =>
        {
            _uiDocument.rootVisualElement.Q<VisualElement>("DimZone").AddToClassList("hidden");
            _uiDocument.rootVisualElement.Q<VisualElement>("DimZone").RemoveFromClassList("show");
            _uiDocument.rootVisualElement.Q<VisualElement>("CreditsBG").AddToClassList("hidden");
            _uiDocument.rootVisualElement.Q<VisualElement>("CreditsBG").RemoveFromClassList("show");
        };
    }

    private void Play()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Tutorial");
        print("PLAY");
        // Credits DimZone Done
    }
    
    private void Credits()
    {
        print("CREDITS");
        _uiDocument.rootVisualElement.Q<VisualElement>("DimZone").RemoveFromClassList("hidden");
        _uiDocument.rootVisualElement.Q<VisualElement>("DimZone").AddToClassList("show");
        _uiDocument.rootVisualElement.Q<VisualElement>("CreditsBG").RemoveFromClassList("hidden");
        _uiDocument.rootVisualElement.Q<VisualElement>("CreditsBG").AddToClassList("show");
    }
    
    
    
    private void AssetShowcase()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
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
