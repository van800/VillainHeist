using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    public AbilityInputs AIS;
    public Battery bat;
    public RawImage theImage;
    public RectTransform pos;

    public static float posx;
    // Start is called before the first frame update
    void Start()
    {
        bat = AIS.bat;
        posx = theImage.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float fraction = ((float) bat.currentBat / (float) bat.maxBat) + 0.0f;
        theImage.rectTransform.localScale = new Vector3(fraction, 1.0f, 1.0f);
        pos.position = new Vector3( posx -((1.0f - fraction) * 42.5f), pos.position.y, pos.position.z);
    }
}
