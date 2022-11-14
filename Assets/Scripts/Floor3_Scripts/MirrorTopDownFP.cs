using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement_and_Camera_Scripts;

public class MirrorTopDownFP : MonoBehaviour
{
	[SerializeField] private GameObject topDownCam;
	[SerializeField] private GameObject fpCam;
	private PlayerController player;
	// Start is called before the first frame update
	void Start()
    {
		player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
		StartCoroutine(switchCam());
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator switchCam()
	{
        while (true)
		{
			topDownCam.SetActive(true);
			fpCam.SetActive(false);
			yield return new WaitUntil(() => player.isFirstPov);
			topDownCam.SetActive(false);
			fpCam.SetActive(true);
			yield return new WaitUntil(() => !player.isFirstPov);
		}
	}
}
