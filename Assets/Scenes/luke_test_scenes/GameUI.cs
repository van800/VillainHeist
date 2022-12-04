using System;
using System.Collections;
using System.Collections.Generic;
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
    
    // Start is called before the first frame update
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        SetBattery(4, 10);
    }

    // Update is called once per frame
    void Update()
    {
        SetBattery(current, total);
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

    public void HideModal(Modals modal)
    {
        
    }
        
    public void SetModal(Modals targetModal)
    {
        // for (var modal in )
        // {
        //     
        // }
        // foreach (var modal in )
        // {
        //     _uiDocument.rootVisualElement.Q<VisualElement>(ModalToId(modal));
        // }
    }
    
    public void SetBattery(int current, int total)
    {
        VisualElement batteryContainer = _uiDocument.rootVisualElement.Q<VisualElement>("BGStart");
        
        print(batteryContainer);
        
        while (batteryContainer.childCount > 0)
        {
            VisualElement batteryIcon = batteryContainer.Children().First();
            batteryIcon.parent.Remove(batteryIcon);
        }

        for (int index = 1; index <= total; index += 1)
        {
            VisualElement elem = new VisualElement();
            if (index == 1) elem.AddToClassList("battery-start");
            else elem.AddToClassList("battery-next");

            if (index > current) elem.AddToClassList("off");

            if (index == total)
            {
                elem.AddToClassList("last");
            }
            batteryContainer.Add(elem);
        }
    }
}
