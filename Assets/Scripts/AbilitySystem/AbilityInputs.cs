using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityInputs : MonoBehaviour
{
    [Header("Ability Start")]
    private AbilityType curType = AbilityType.Clickable;
    [SerializeField]
    private string buttonToActivate = "Fire1";
    [SerializeField]
    private string buttonToSelect = "Fire1";
    [Header("Abilities")]
    private Ability[] abilities;
    private int abilityIndexUsed = 0;
    public enum AbilityType { Clickable };
    [Header("Selection")]
    private bool movingSelection;
    [SerializeField]
    private float selectionTimeScale = 0f;
    private Vector3 curSelectionLoc;
    [SerializeField]
    private float selectionMoveSpeed = 2f;
    private Vector3 prevMousePos;
    [SerializeField]
    private GameObject selectionPrefab;
    private GameObject curSelectionObj;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Ability System Running");
        movingSelection = false;
        ResetAbilityComponents();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStart();
    }

    private void CheckStart()
    {
        if (Input.GetButtonDown(buttonToActivate))
        {
            Debug.Log("Check Start Complete");
            StartCoroutine(AbilityRoutine());
        }
    }

    private IEnumerator AbilityRoutine()
    {
        yield return new WaitUntil(() => !Input.GetButtonUp(buttonToSelect));
        // This will prevent the user from accidentally immediatly selecting
        // a location.
        Debug.Log("AbilityRoutine Start");
        float oldTimeScale = Time.timeScale;
        Time.timeScale = selectionTimeScale;
        //curSelectionLoc = 
        StartCoroutine(MoveSelection());
        yield return new WaitUntil(() => !movingSelection);
    }

    private IEnumerator MoveSelection()
    {
        movingSelection = true;
        while (movingSelection)
        {
            Debug.Log("VisualizeSelection Running");
            if (MouseHasMoved())
            {
                curSelectionLoc = Input.mousePosition;
            }
            else
            {
                Vector3 input = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                curSelectionLoc += input * selectionMoveSpeed * Time.unscaledDeltaTime;
            }
            Camera.main.ScreenToWorldPoint(curSelectionLoc);
            ShowSelectionWithObj();
            if (Input.GetButtonUp(buttonToSelect))
            {
                movingSelection = false;
            }
            Debug.Log("curSelectionLoc = (" + curSelectionLoc.x + ", " + curSelectionLoc.y + ")");

            yield return new WaitForEndOfFrame();
        }
        Debug.Log("VisualizeSelection Complete");
        RemoveSelectionObj();
    }

    private void ShowSelectionWithObj()
    {
        if (curSelectionObj == null)
        {
            curSelectionObj = Instantiate(selectionPrefab);
        }
        curSelectionObj.transform.position = curSelectionLoc;
    }

    private void RemoveSelectionObj()
    {
        Destroy(curSelectionObj);
    }

    private bool MouseHasMoved()
    {
        if ((Input.mousePosition - prevMousePos).magnitude > 0)
        {
            Debug.Log("Mouse is moving");
            prevMousePos = Input.mousePosition;
            return true;
        }
        return false;
    }

    private void ResetAbilityComponents()
    {
        abilities = GetComponents<Ability>();
        abilityIndexUsed = 0;
    }
}

public interface Ability
{
    /**
     * Where this Ability is to be used.
     */
    public void ApplyTo(Vector3 position);
}
