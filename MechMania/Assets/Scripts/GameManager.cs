using UnityEngine;
using UnityEngine.UI; //For UI elements

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying; //Start music
    //public BeatScroller theBS; //Reference to BeatScroller script
    public static GameManager instance;

    //Note score variables
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    //Game tracking variables
    //MAKE THESE PRIVATE AFTER TESTING
    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    //Results display
    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    //UI elements to display
    public Text scoreText;
    public Text multiText; //Score multiplier for combos

    //Score multiplier variables
    public int currentMultiplier;
    public int multiplierTracker; //Tacks combo to threshold
    public int[] multiplierThresholds; //Each level of thhe multipler is harder to achieve than the previous



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this; //Only ever have on game manager
        scoreText.text = "Score: 0";
        currentMultiplier = 1; //Don't want to be multiplying by 0!

        totalNotes = FindObjectsByType<NoteObject>(FindObjectsSortMode.None).Length; //Counts all notes
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying) {
            if (Input.anyKeyDown) {
                startPlaying = true;
                //theBS.hasStarted = true; //Start scrolling
                theMusic.Play(); //Start music
            }
        }
        else {
            if (!theMusic.isPlaying && !resultsScreen.activeInHierarchy) { //Song must have ended (music finished and no results screen up yet)
                resultsScreen.SetActive(true);

                normalsText.text = normalHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = perfectHits.ToString();

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;
                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if (percentHit > 40) {
                    rankVal = "D";
                    if (percentHit > 55) {
                        rankVal = "C";
                        if (percentHit > 70) {
                            rankVal = "B";
                            if (percentHit > 85) {
                                rankVal = "A";
                                if (percentHit > 95) {
                                    rankVal = "S";
                                    if (percentHit > 99) {
                                        rankVal = "SS";
                                    }
                                }
                            }
                        }
                    }
                }
                rankText.text = rankVal;
                finalScoreText.text = currentScore.ToString();
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

        // currentScore += scorePerNote * currentMultiplier; //Increase score
        scoreText.text = "Score: " + currentScore; //Update score text
    }

    public void NormalHit() {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit() {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit() {
        currentScore += scorePerPerfectNote * currentMultiplier; 
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed() {
        Debug.Log("Note Missed!");

        //Reset multiplier
        currentMultiplier = 1;
        multiplierTracker = 0;

        missedHits++;

        multiText.text = "Multiplier: x" + currentMultiplier; //Update multiplier text
    }
}
