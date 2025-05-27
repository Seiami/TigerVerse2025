using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using static NoteObject;


//https://www.youtube.com/watch?v=EY7IdBDl4PI&t=55s
public class ReadBeatMap : MonoBehaviour
{
    public static ReadBeatMap Instance; //Singleton instance for easy access

    public int Count; //Counter for the number of notes read
    public List<List<float>> Notes { get; private set; } = new List<List<float>>(); //So we can access the notes from other scripts without modifying them directly/accidentally
   
    void Awake() => Instance = this; //Set the singleton instance


    //[SerializeField] public NoteObject _noteObject; //Note prefab to spawn
    //public Transform _emitter; //Parent object to attach the notes to

    void Start()
    {
        Read(); //Call the Read method to read the BeatMap file
    }

    public void Read()
    {
        Debug.Log("START: Reading File");
        Count = 0; //Reset the note count

        string filePath = Application.streamingAssetsPath + "/BeatMaps/" + "TestBeatMap" + ".txt"; //Path to the BeatMap file

        List<string> lines = new List<string>();

        lines = File.ReadAllLines(filePath).ToList(); //Read all lines from the BeatMap file into a list

        foreach (string line in lines)
        {
            Count++;
            string[] items = line.Split(','); //Split the line by commas
            if (items.Length >= 3)
            { //Check if there are at least 3 elements (time, note, duration)
                float time = float.Parse(items[0]); //Parse the time in eighth notes from start of song
                float type = float.Parse(items[1]); //Parse the note from the second element
                float duration = float.Parse(items[2]); //Parse the duration from the third element
                Notes.Add(new List<float> { time, type, duration }); //Add the note to the notes list
            }
        }
        Debug.Log("END: Reading File");

    }
}
