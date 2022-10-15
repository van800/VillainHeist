using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOffAbility : MonoBehaviour, Ability
{
    [SerializeField] private string name = "Lights Off Ability";
    [SerializeField] private Sprite icon;

    public AbilityInputs.AbilityType abilityType1stPerson()
    {
        return AbilityInputs.AbilityType.Shootable;
    }

    public AbilityInputs.AbilityType abilityType3rdPerson()
    {
        return AbilityInputs.AbilityType.Clickable;
    }

    public AbilityInputs.AbilityTarget abilityTarget()
    {
        return AbilityInputs.AbilityTarget.SelectableObject;
    }

    public void ApplyTo(GameObject spot)
    {
        OvalLightController spotsLight;
        if (spot.TryGetComponent(out spotsLight))
        {
            //spotsLight.
        }
    }

    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetName()
    {
        return name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
