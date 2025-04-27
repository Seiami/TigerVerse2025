using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser; 
    AudioSource sound;
    bool isPressed;

    // Changing sphere color
    // private Renderer sphereRenderer;
    // private Color newSphereColor; 
    // private float randomChannelOne, randomChannelTwo, randomChannelThree;

    // Start is called before the first frame update
    void Start()
    {
        // sphereRenderer = sphere.GetComponent<Renderer>(); // I 
        // gameObject.GetComponent<button>().onClick.AddListener(ChangeSphereColor);
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void onTriggerEnter(Collider other)
    {
        if(!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.003f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void onTriggerExit(Collider other)
    {
        if(other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0,0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }
    // private void ChangeSphereColor(){
    //     randomChannelOne = Random.Range(0f, 1f);
    //     randomChannelTwo = Random.Range(0f, 1f);
    //     randomChannelThree = Random.Range(0f, 1f);
        
    //     newSphereColor = new Color(randomChannelOne, randomChannelTwo, randomChannelThree);
    // }
    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();

        // sphereRenderer.material.SetColor("_Color", newSphereColor);
    }

}
