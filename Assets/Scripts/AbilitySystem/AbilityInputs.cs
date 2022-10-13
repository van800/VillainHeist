using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInputs : MonoBehaviour
{
    private bool running;
    [Header("Ability Start")]
    [SerializeField] [Tooltip("Probably the player transform")]
    private Transform abilitySelectionStartTransform;
    [SerializeField]
    private string buttonToActivate = "Fire1";
    [SerializeField]
    private string buttonToSelect = "Fire1";
    [Header("Abilities")]
    private Ability[] abilities;
    public enum AbilityType { Clickable, Immediate };
    [Header("Ability Selection")]
    [SerializeField]
    private GameObject activeCanvas;
    [SerializeField]
    private Transform activeCanvasContent;
    [SerializeField]
    private Button abilityButtonPreFab;
    private IEnumerator curAbilitySelRoutine;
    // This is the current ability selection routine
    private AbilityType curType = AbilityType.Clickable;
    private int abilityIndexUsed = 0;
    [Header("Clickable Selection")]
    private bool movingSelection;
    [SerializeField]
    private float selectionTimeScale = 0f;
    private Vector3 curSelectionLoc;
    [SerializeField]
    private float selectionMoveSpeed = 2f;
    [SerializeField]
    private float mouseSelectionMoveSpeed = 1f;
    [SerializeField]
    private float minMouseMove = .1f;
    [SerializeField]
    private GameObject selectionPrefab;
    private GameObject curSelectionObj;
    [Header("Immediate Selection")]
    [SerializeField]
    private GameObject immediateCanvas;
    private bool selectedImmediateOption;
    private bool useImmediate;
    [SerializeField]
    private string immediateCanvasQuestion = "Use [Ability]?";
    [SerializeField]
    private Text immediateCanvasText;

    // Start is called before the first frame update
    void Start()
    {
        selectedImmediateOption = false;
        running = false;
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
        if (!movingSelection && Input.GetButtonDown(buttonToActivate))
        {
            ResetAbilityComponents();
            AbilitySystemStart();
            activeCanvas.SetActive(true);
        }
        if (running)
        {
            SwitchAbilities();
        }
    }

    private void AbilitySystemStart()
    {
        AbilityType prevType = curType;
        curType = abilities[abilityIndexUsed].abilityType();
        if (!running || prevType != curType)
        {
            running = true;
            if (prevType == AbilityType.Clickable)
            {
                ClickableStop();
            }
            else if (prevType == AbilityType.Immediate)
            {
                ImmediateStop();
            }

            if (curType == AbilityType.Clickable)
            {
                curAbilitySelRoutine = ClickableRoutine();
            }
            else if (curType == AbilityType.Immediate)
            {
                curAbilitySelRoutine = ImmediateRoutine();
            }
            StartCoroutine(curAbilitySelRoutine);
        }
    }

    private IEnumerator ImmediateRoutine()
    {
        ShowImmediateCanvas();
        yield return new WaitUntil(() => selectedImmediateOption);
        immediateCanvas.SetActive(false);
        if (useImmediate)
        {
            abilities[abilityIndexUsed].ApplyTo(curSelectionLoc);
        }
        AbilitySystemEnd();
    }

    private void ImmediateStop()
    {
        selectedImmediateOption = true;
        useImmediate = false;
        //immediateCanvas.SetActive(false);
    }

    private void ShowImmediateCanvas()
    {
        selectedImmediateOption = false;
        immediateCanvas.SetActive(true);
        int abilityNameStart = immediateCanvasQuestion.IndexOf("[Ability]");
        int abilityNameEnd = immediateCanvasQuestion.IndexOf("[Ability]") + "[Ability]".Length;
        string curQuestion = immediateCanvasQuestion.Substring(0, abilityNameStart)
            + abilities[abilityIndexUsed].GetName()
            + immediateCanvasQuestion.Substring(abilityNameEnd);
        immediateCanvasText.text = curQuestion;
    }

    public void selectImmediateOption(bool yes)
    {
        useImmediate = yes;
        selectedImmediateOption = true;
    }

    private IEnumerator ClickableRoutine()
    {
        yield return new WaitUntil(() => Input.GetButtonUp(buttonToSelect));
        // This will prevent the user from accidentally immediatly selecting
        // a location.
        float oldTimeScale = Time.timeScale;
        Time.timeScale = selectionTimeScale;
        curSelectionLoc = abilitySelectionStartTransform.position;
        StartCoroutine(MoveSelection());
        yield return new WaitUntil(() => !movingSelection);
        Time.timeScale = oldTimeScale;
        AbilitySystemEnd();
    }

    private IEnumerator MoveSelection()
    {
        movingSelection = true;
        while (movingSelection)
        {
            Vector3 input;
            if (MouseHasMoved())
            {
                input = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));
                input *= mouseSelectionMoveSpeed;
            }
            else
            {
                input = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                input *= selectionMoveSpeed;
            }
            curSelectionLoc += input * Time.unscaledDeltaTime;
            //Camera.main.ScreenToWorldPoint(curSelectionLoc);
            ShowSelectionWithObj();
            if (Input.GetButton(buttonToSelect))
            {
                movingSelection = false;
            }

            yield return new WaitForEndOfFrame();
        }
        if (curType == AbilityType.Clickable)
        {
            RemoveSelectionObj();
            abilities[abilityIndexUsed].ApplyTo(curSelectionLoc);
            //AbilitySystemEnd();
        }
    }

    private void ClickableStop()
    {
        RemoveSelectionObj();
        movingSelection = false;
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
        return new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y")).magnitude > minMouseMove;
    }

    private void ResetAbilityComponents()
    {
        abilities = GetComponents<Ability>();
        abilityIndexUsed = 0;
        ResetActiveCanvasButtons();
    }

    private void ResetActiveCanvasButtons()
    {
        ClearActiveCanvasButtons();
        for (int i = 0; i < abilities.Length; i++)
        {
            GameObject abilityButtonObj = Instantiate(abilityButtonPreFab.gameObject, activeCanvasContent);
            Button abilityButton = abilityButtonObj.GetComponent<Button>();
            int abilityNum = i;
            abilityButton.onClick.AddListener(() => { SwitchAbilityTo(abilityNum); });
            // Adds the function to this button to change the current ability to
            // this button's ability
            abilityButtonObj.transform.GetChild(0).GetComponent<Image>().sprite = abilities[i].GetIcon();
            abilityButtonObj.GetComponentInChildren<Text>().text = "" + (abilityNum + 1);
            // This will work as long as the only child object of the ability button is its sprite
        }
    }

    private void ClearActiveCanvasButtons()
    {
        for (int index = 0; index < activeCanvasContent.GetChildCount(); index++)
        {
            Destroy(activeCanvasContent.GetChild(index).gameObject);
        }
    }

    private void SwitchAbilities()
    {
        string input = Input.inputString;
        for (int i = 0; i < abilities.Length; i++)
        {
            if (input != null && input.Equals("" + (i + 1)))
            {
                SwitchAbilityTo(i);
            }
        }
    }

    private void SwitchAbilityTo(int i)
    {
        if (i < 0 || i >= abilities.Length)
        {
            throw new AbilityIndexOutOfBoundsException();
        }
        abilityIndexUsed = i;
        AbilitySystemStart();
    }

    private void AbilitySystemEnd()
    {
        running = false;
        activeCanvas.SetActive(false);
    }
}

// Thrown when you try changing the ability index to an index that does not exist
class AbilityIndexOutOfBoundsException : System.Exception {

}

public interface Ability
{
    /**
     * Where this Ability is to be used.
     */
    public void ApplyTo(Vector3 position);

    public AbilityInputs.AbilityType abilityType();

    public string GetName();

    public Sprite GetIcon();
}
