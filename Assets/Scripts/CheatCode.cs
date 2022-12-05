using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement_and_Camera_Scripts;

public class CheatCode : MonoBehaviour
{
    [SerializeField] private string moreBatPassword = "frogs";
    [SerializeField] private int moreBatVal = 24;
    [SerializeField] private bool isFirstPersonCheat;
    private bool usedCheat = false;
    private string nextChar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(moreBatPasswordCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator moreBatPasswordCheck()
    {
        while (!usedCheat)
        {
            string curString = "";
            bool notWrong = true;
            while (notWrong) {
                yield return new WaitUntil(() => !userTypedSomething());
                yield return new WaitUntil(() => userTypedSomething());
                curString += nextChar;
                if (curString.Equals(moreBatPassword))
                {
                    usedCheat = true;
                    if (!isFirstPersonCheat)
                    {
                        GameState.Instance.updateTotalBat(moreBatVal);
                    }
                    else
                    {
                        if (!GameState.Instance.isInFirstPerson)
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ToPov();
                        }
                    }
                }
                else if (!moreBatPassword.StartsWith(curString, System.StringComparison.CurrentCultureIgnoreCase))
                {
                    notWrong = false;
                }
            }
        }
    }

    private bool userTypedSomething()
    {
        if (Input.inputString != null && !Input.inputString.Equals(""))
        {
            nextChar = Input.inputString;
            return true;
        }
        return false;
    }
}
