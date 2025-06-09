using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv
public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public float time;
    public float type;
    public float duration;
    private UnityEngine.Events.UnityAction<SelectEnterEventArgs> cachedListener;


    //Hit effects
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //FindObjectOfType<XRSimpleInteractable>().selectEntered.AddListener(OnButtonPress);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            HandleNoteHit();
        }
    }

    public void OnButtonPress()
    { //Method button calls
        HandleNoteHit();
    }

    private void HandleNoteHit()
    {
        if (canBePressed)
        {
            gameObject.SetActive(false);
            //GameManager.instance.NoteHit();
            if (Mathf.Abs(transform.position.y) > 1.66f)
            { //0.25 off zero in either direction
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                Debug.Log("Hit!");
            }
            else if (Mathf.Abs(transform.position.y) > 1.51f)
            {
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                Debug.Log("Good Hit!");
            }
            else if (Mathf.Abs(transform.position.y) < 1.16f)
            {
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, goodEffect.transform.rotation);
                Debug.Log("Normal Hit!");
            }
            else if (Mathf.Abs(transform.position.y) < 1.31f)
            {
                GameManager.instance.GoodHit();
                Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                Debug.Log("Good Hit!");
            }
            else
            {
                Debug.Log("Perfect Hit!");
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                GameManager.instance.PerfectHit();
            }
            RemoveNoteFromXRButton(); //Remove the note's OnButtonPress to the selectEntered event of the XR Interactable
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);
        if (other.tag == "Activator")
        {
            canBePressed = true;
            AddNoteToXRButton(); //Adds the note's OnButtonPress to the selectEntered event of the XR Interactable
            Debug.Log("canBePressed set to true");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called with: " + other.gameObject.name);
        if (other.tag == "Activator")
        {
            canBePressed = false;
            Debug.Log("canBePressed set to false");
            RemoveNoteFromXRButton(); //Remove the note's OnButtonPress to the selectEntered event of the XR Interactable
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            Destroy(gameObject);
        }
    }

    /// Adds the note's OnButtonPress to the selectEntered event of the given XR Interactable GameObject.
    public void AddNoteToXRButton()
    {
        //var interactable = xrButtonObject.GetComponent<XRSimpleInteractable>();
        ButtonType[] buttons = FindObjectsOfType<ButtonType>();
        if (buttons != null)
        {
            foreach (ButtonType button in buttons)
            {
                if (button.type == type)
                {
                    GameObject xrButtonObject = button.gameObject; //Get the GameObject of the button

                    /*
                    //XRSimpleInteractable xrInteractable = xrButtonObject.GetComponent<XRSimpleInteractable>(); //Get the XR Interactable component from the button GameObject
                    xrButtonObject.GetComponent<XRSimpleInteractable>().selectEntered.AddListener((args) => note.OnButtonPress()); //Adds the note's OnButtonPress to the selectEntered event of the XR Interactable         
                    */

                    //In case of future object pooling and/or failure in removal
                    if (cachedListener != null)
                    {
                        xrButtonObject.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(cachedListener);
                    }

                    cachedListener = (args) => OnButtonPress();
                    xrButtonObject.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(cachedListener);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("XR Interactable or NoteObject missing!");
        }
    }

    public void RemoveNoteFromXRButton()
    {
        ButtonType[] buttons = FindObjectsOfType<ButtonType>();
        if (buttons != null && cachedListener != null)
        {
            foreach (ButtonType button in buttons)
            {
                if (button.type == type)
                {
                    GameObject xrButtonObject = button.gameObject; //Get the GameObject of the button
                    xrButtonObject.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(cachedListener);
                    cachedListener = null; // Clear the cached listener to prevent memory leaks
                    break;
                }
            }
        }
    }

    /*private void OnTriggerEnter(Collider other) {
        if (other.tag == "Activatior") {
           canBePressed = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Activatior") {
           canBePressed = false;
        }
        GameManager.instance.NoteMissed();
        Instantiate(missEffect, transform.position, missEffect.transform.rotation);
    }*/
}

