using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTaggedObjsAbility : MonoBehaviour, Ability
{
    [SerializeField] private string tag;

    public AbilityInputs.AbilityType abilityType()
    {
        return AbilityInputs.AbilityType.Immediate;
    }

    public void ApplyTo(Vector3 position)
    {
        GameObject[] taggedObjs = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in taggedObjs)
        {
            Destroy(obj);
        }
    }

    public string GetName()
    {
        return "ClearTaggedObjsAbility";
    }

    public Sprite GetIcon()
    {
        return null;
        // Replace with debug sprite
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
