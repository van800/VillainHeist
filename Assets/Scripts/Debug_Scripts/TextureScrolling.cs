using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScrolling : MonoBehaviour
{
    //[SerializeField] private string textureID;
    [SerializeField] private Vector2 speed = new Vector2(.1f, .1f);
    private Material mat;
    private Vector2 offset;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        offset = mat.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollTexture();
    }

    private void ScrollTexture()
    {
        offset.x += speed.x * Time.deltaTime;
        offset.y += speed.y * Time.deltaTime;
        mat.mainTextureOffset = offset;
    }
}
