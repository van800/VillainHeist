using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    
    private UIDocument _uiDocument;

    public int current = 0;
    public int total = 10;

    public enum Modals
    {
        Welcome,
        Controls,
        TimesUp,
        Victory
    }
    
    public enum AbilityPrompts
    {
        Freeze,
        Light,
        Pickup,
        MoveWall
    }

    private Modals[] ALL_MODALS = {
        Modals.Controls, Modals.Welcome, Modals.Victory, Modals.TimesUp
    };
    
    private AbilityPrompts[] ALL_ABILITY_PROMPTS = {
        AbilityPrompts.Freeze,
        AbilityPrompts.Light,
        AbilityPrompts.Pickup,
        AbilityPrompts.MoveWall
    };
    
    // Start is called before the first frame update
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        HideAllModals();
        HideAllAbilityPrompts();

        SetBattery(6, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private string ModalToId(Modals modal)
    {
        return modal switch
        {
            Modals.Welcome => "Welcome",
            Modals.Controls => "Controls",
            Modals.TimesUp => "TimesUp",
            Modals.Victory => "Escaped",
            _ => throw new NotImplementedException("Invalid Enum Value!")
        };
    }
    
    private string AbilityToId(AbilityPrompts modal)
    {
        return modal switch
        {
            AbilityPrompts.Freeze => "Freeze",
            AbilityPrompts.Light => "Light",
            AbilityPrompts.Pickup => "Pickup",
            AbilityPrompts.MoveWall => "MoveWall",
            _ => throw new NotImplementedException("Invalid Enum Value!")
        };
    }

    private void HideAllAbilityPrompts()
    {
        foreach (var abilityPrompt in ALL_ABILITY_PROMPTS)
        {
            _uiDocument.rootVisualElement.Q<VisualElement>(AbilityToId(abilityPrompt)).AddToClassList("hidden");
        }
    }
    
    private void ShowAbilityPrompts(AbilityPrompts targetPrompt)
    {
        foreach (var abilityPrompt in ALL_ABILITY_PROMPTS)
        {
            if (targetPrompt == abilityPrompt)
            {
                _uiDocument.rootVisualElement.Q<VisualElement>(AbilityToId(abilityPrompt)).RemoveFromClassList("hidden");
            }
            else
            {
                _uiDocument.rootVisualElement.Q<VisualElement>(AbilityToId(abilityPrompt)).AddToClassList("hidden");
            }
        }
    }
    
    private void HideAllModals()
    {
        foreach (var modal in ALL_MODALS)
        {
            _uiDocument.rootVisualElement.Q<VisualElement>(ModalToId(modal)).AddToClassList("hidden");
        }
    }

    public void ShowCutsceneText()
    {
        ShowForSeconds("CutsceneText", 2);
    }

    private void ShowForSeconds(string id, float time)
    {
        _uiDocument.rootVisualElement.Q<VisualElement>(id).RemoveFromClassList("hidden");
        StartCoroutine(HideAfterDelay(id, time));
    }
    
    public void ShowCheckpoint()
    {
        ShowForSeconds("Checkpoint", 2);
    }

    private IEnumerator HideAfterDelay(string id, float delay)
    {
        yield return new WaitForSeconds(delay);
        _uiDocument.rootVisualElement.Q<VisualElement>(id).AddToClassList("hidden");
    }
    
    public void HideModal(Modals modal)
    {
        _uiDocument.rootVisualElement.Q<VisualElement>(ModalToId(modal)).RemoveFromClassList("hidden");
    }
        
    public void SetModal(Modals targetModal)
    {
        foreach (var modal in ALL_MODALS)
        {
            if (modal == targetModal)
            {
                _uiDocument.rootVisualElement.Q<VisualElement>(ModalToId(modal)).RemoveFromClassList("hidden");
            }
            else
            {
                _uiDocument.rootVisualElement.Q<VisualElement>(ModalToId(modal)).AddToClassList("hidden");
            }
        }
    }
    
    public void SetBattery(int currentBattery, int totalBattery)
    {
        VisualElement batteryContainer = _uiDocument.rootVisualElement.Q<VisualElement>("BGStart");

        while (batteryContainer.childCount > 0)
        {
            VisualElement batteryIcon = batteryContainer.Children().First();
            batteryIcon.parent.Remove(batteryIcon);
        }

        for (int index = 1; index <= totalBattery; index += 1)
        {
            VisualElement elem = new VisualElement();
            if (index == 1) elem.AddToClassList("battery-start");
            else elem.AddToClassList("battery-next");

            if (index > currentBattery) elem.AddToClassList("off");

            if (index == totalBattery)
            {
                elem.AddToClassList("last");
            }
            batteryContainer.Add(elem);
        }
    }
}
