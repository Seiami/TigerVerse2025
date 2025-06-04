using UnityEngine;

//https://www.youtube.com/watch?v=cZzf1FQQFA0&list=PLLPYMaP0tgFKZj5VG82316B63eet0Pvsv
public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public float time;
    public float type;
    public float duration;

    //Hit effects
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
            if (Mathf.Abs(transform.position.y) > 2.15f)
            { //0.25 off zero in either direction
                GameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                Debug.Log("Hit!");
            }
            else if (Mathf.Abs(transform.position.y) > 2f)
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name);
        if (other.tag == "Activator")
        {
            canBePressed = true;
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
            GameManager.instance.NoteMissed();
            gameObject.SetActive(false);
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            Destroy(gameObject);
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
