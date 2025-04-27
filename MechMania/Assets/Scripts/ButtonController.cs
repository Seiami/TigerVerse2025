using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR; //Render button
    public Sprite defaultImage; //Normal button image
    public Sprite pressedImage;

    public KeyCode keyToPress; //Key we want to press down

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress)) {
            theSR.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress)) {
            theSR.sprite = defaultImage;
        }
    }
}
