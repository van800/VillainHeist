using System.Collections;
using System.Collections.Generic;
using movement_and_Camera_Scripts;
using UnityEngine;

public class PasswordPiece : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 5f;
    [SerializeField] private int pieceNum = -1;
    [SerializeField] private GameObject child;
    private static HashSet<int> piecesFound;

    // Start is called before the first frame update
    void Awake()
    {
        if (piecesFound == null)
        {
            piecesFound = new HashSet<int>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        spin();
    }

    private void spin()
    {
        child.transform.eulerAngles += new Vector3(0, spinSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            piecesFound.Add(pieceNum);
            GameObject villain = GameObject.Find("villain-prime");
            PlayerController pc = villain.GetComponent<PlayerController>();
            pc.PlayPickupSound();
            Debug.Log("Found " + pieceNum + " star piece");
            Destroy(gameObject);
        }
    }

    public static bool hasBeenFound(int num)
    {
        return piecesFound.Contains(num);
    }
}
