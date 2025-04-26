using UnityEngine;
using UnityEngine.UI; //For UI elements

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying; //Start music
    public BeatScroller theBS; //Reference to BeatScroller script
    public static GameManager instance;
    public int currentScore;
    public int scorePerNote = 100;

    public Text scoreText;
    public Text multiText; //Score multiplier for combos

    public int currentMultiplier;
    public int multiplierTracker; //Tacks combo to threshold
    public int[] multiplierThresholds; //Each level of thhe multipler is harder to achieve than the previous



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this; //Only ever have on game manager
        scoreText.text = "Score: 0";
        currentMultiplier = 1; //Don't want to be multiplying by 0!
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying) {
            if (Input.anyKeyDown) {
                startPlaying = true;
                theBS.hasStarted = true; //Start scrolling
                theMusic.Play(); //Start music
            }
        }
    }

    public void NoteHit() {
        Debug.Log("Note Hit!");

        //Ensure we do not run over end of array, only increase multiplier if we have not reached highest mult
        if (currentMultiplier - 1 < multiplierThresholds.Length) {
            multiplierTracker++; //Increase the tracker for the multiplier
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker) {
                multiplierTracker = 0;
                currentMultiplier++; //Increase the multiplier
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier; //Update multiplier text

        currentScore += scorePerNote * currentMultiplier; //Increase score
        scoreText.text = "Score: " + currentScore; //Update score text
    }

    public void NoteMissed() {
        Debug.Log("Note Missed!");

        //Reset multiplier
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier; //Update multiplier text
    }
}
