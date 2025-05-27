using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=mAiQJoblHh4

public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("0 Start Scene");
    }
}
