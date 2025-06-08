using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Select : MonoBehaviour
{
    public static int song; // 1 is scene1 2 is scene2
    public TMP_Text buttonText;
    // Start is called before the first frame update
    void Start() {
        song = 1;
    }
    public void SongSelect() {
        if (song == 1) {
            song = 2;
            buttonText.text = "Gothic Lofi";
            return;
        }
        song = 1;
        buttonText.text = "Funky Lofi";

    }
}
