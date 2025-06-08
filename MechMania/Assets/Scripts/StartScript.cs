using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public Level_Select Level_Select;

    public void OpenScene() {
        if (Level_Select.song == 1) { // delta rune vibes
            SceneManager.LoadScene("1 Song1 Cockpit");
            return;
        }
        if (Level_Select.song == 2) {
            SceneManager.LoadScene("2 Song2 Cockpit v2");
            return;
        }



    }
}
