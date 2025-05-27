using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo; //Fall speed
    public bool hasStarted; //Will press button to make it start scrolling


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatTempo = beatTempo / 60f; //Speed per sec

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) {
            /*if(Input.anyKeyDown) {
                hasStarted = true;
            }*/
        }
        else {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
