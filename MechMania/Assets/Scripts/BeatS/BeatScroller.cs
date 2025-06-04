using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv
public class BeatScroller : MonoBehaviour
{
    [SerializeField] BeatManager beatManager; //Reference to the BeatManager
    private float beatTempo; //Fall speed
    public bool hasStarted; //Will press button to make it start scrolling


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beatManager = FindObjectOfType<BeatManager>(); //Find the BeatManager in the scene
        beatTempo = beatManager.BPM / 60f; //Speed per sec

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted || false)
        {
            /*if(Input.anyKeyDown) {
                hasStarted = true;
            }*/
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
