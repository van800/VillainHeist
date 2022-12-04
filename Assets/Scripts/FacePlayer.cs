using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using movement_and_Camera_Scripts;

public class FacePlayer : MonoBehaviour
{
    private PlayerController player;
    private Quaternion oldRotation;
    [SerializeField] private float scaleWithPlayerRate = 1;
    private Vector3 initScale;
    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        oldRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isFirstPov)
        {
            updateRotation();
            updateScale();
        }
        else
        {
            transform.rotation = oldRotation;
            transform.localScale = initScale;
        }
    }

    private void updateRotation()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    private void updateScale()
    {
        if (scaleWithPlayerRate != 0)
        {
            transform.localScale = initScale * (scaleWithPlayerRate * (transform.position - player.transform.position).magnitude);
        }
    }
}
