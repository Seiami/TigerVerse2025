using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=2BdgUgh_yxA
public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void quitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
