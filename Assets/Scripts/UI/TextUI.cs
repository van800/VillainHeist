using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    [SerializeField]
    public AbilityInputs abilityInputSystem;
    
    [SerializeField]
    private Battery batt;
    public TextMeshProUGUI theText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
       // this.batt = abilityInputSystem.bat;
    }

    // Update is called once per frame
    void Update()
    {
        theText.text = batt.currentBat + " / " + batt.maxBat + " V";
        //theText.text = "Subscribe";
    }
}
