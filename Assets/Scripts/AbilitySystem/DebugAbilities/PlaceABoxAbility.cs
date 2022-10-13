using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceABoxAbility : MonoBehaviour, Ability
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float yRaycastPositionInc = 2f;

    public void ApplyTo(Vector3 position)
    {
        RaycastHit hit;
        Vector3 raycastStartPos = position;
        raycastStartPos.y += yRaycastPositionInc;
        bool hitFloor = Physics.Raycast(raycastStartPos, Vector3.down, out hit);
        GameObject newObj = Instantiate(obj);
        if (hitFloor) {
            position.y = hit.transform.position.y;
            position.y += hit.collider.bounds.extents.y;
            position.y += newObj.transform.localScale.y / 2;
        }
        newObj.transform.position = position;
        newObj.transform.tag = "Respawn";
    }

    public AbilityInputs.AbilityType abilityType()
    {
        return AbilityInputs.AbilityType.Clickable;
    }

    public string GetName()
    {
        return "PlaceABoxAbility";
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
