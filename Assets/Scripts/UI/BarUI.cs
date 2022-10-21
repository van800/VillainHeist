using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{

    [SerializeField]
    public Battery batt;

    [SerializeField]
    public RawImage theImage;
    
    //[SerializeField]
    //public RectTransform pos;

    //public static float posx;

    private float fraction;

    // Start is called before the first frame update

    void Start()

    {
      //  posx = theImage.transform.position.x;
        

        //posx = theImage.transform.position.x;

    }


// Update is called once per frame
    void Update()
    {
        fraction = ((float) batt.currentBat / (float) batt.maxBat) + 0.0f;
        theImage.rectTransform.localScale = new Vector3(fraction, 1.0f, 1.0f);

        //pos.position = new Vector3( posx -((1.0f - fraction) * 42.5f), pos.position.y, pos.position.z);
        

        //pos.position = new Vector3( posx -((1.0f - fraction) * 42.5f), pos.position.y, pos.position.z);

    }
}
