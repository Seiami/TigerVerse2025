using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//https://www.youtube.com/watch?v=gIjajeyjRfE
//https://www.youtube.com/watch?v=dtv7mjj_iog

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    public float BPM => _bpm; //Public property to access BPM
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    //static int index = 0; //Static index to keep track of the current note being processed
    static bool processed = false;
    int[] indices = new int[4]; //Array to store indices for each interval type

    private void Start()
    {
        if (ReadBeatMap.Instance == null)
        {
            Debug.LogError("ERROR: Not creating beatmap.");
            return; //Exit if the BeatMap has not been read
        }

        int z = 1; //Initialize z to 1, which represents the first note type
        for (int y = 0; y < 4; y++)
        {
            indices[y] = ReadBeatMap.Instance.Notes.FindIndex(x => x[1] == z && x[0] >= 0); //Find the index of the next note that matches the current interval type
            z++;
        }
    }

    private void Update()
    {
        if (_audioSource == null || _intervals.Length == 0 || ReadBeatMap.Instance.Notes.Count == 0)
        {
            Debug.LogError("ERROR: BeatManager is not set up correctly.");
            return; //Exit if the AudioSource, intervals, or notes are not set up
        }
        else
        {
            int index = 0;
            foreach (Intervals interval in _intervals)
            {
                float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm))); //Gets time elapsed in intervals
                int iSampledTime = Mathf.FloorToInt(sampledTime); //Round down to the nearest whole number

                if (indices[index] >= ReadBeatMap.Instance.Notes.Count) //Check if the index exceeds the number of notes
                {
                    //indices[index] = 0; //Reset index if it exceeds
                }
                else
                {
                    processed = interval.CheckForNewInterval(iSampledTime, ReadBeatMap.Instance.Notes[indices[index]][1], ReadBeatMap.Instance.Notes[indices[index]][0]); //Check if we have crossed into a new interval

                    if (processed) //Ensure note is printed before moving on to next
                    {
                        int temp = ReadBeatMap.Instance.Notes.FindIndex(x => x[1] == interval._noteType && x[0] >= sampledTime); //Find the index of the next note that matches the current interval type
                        if (temp != -1) //If a valid index is found
                        {
                            indices[index] = temp; //Update the index to the next note
                        }
                        else
                        {
                            indices[index] = ReadBeatMap.Instance.Notes.Count; //Set to count if no more notes are found
                        }
                        processed = false; //Reset processed flag
                    }
                }

                index++;           
            }
        }
    }
}

[System.Serializable]
public class Intervals
{ //Interval = beat length
    [SerializeField] private float _steps; //Intervals (halfnotes, whole, etc?)
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval; //From last funct call
     [SerializeField] public int _noteType; //Type of note

    public float GetIntervalLength(float bpm) //Gets length of current beat
    {
        return 60f / (bpm * _steps);
    }

    public bool CheckForNewInterval(float interval, float type, float time) //Checks if we have crossed into a new beat
    {
        bool proc = false; //processed
        //FloortoInt rounds DOWN to the nearest whole number
        if (Mathf.FloorToInt((interval)) != _lastInterval) //Check every whole number --> we have passed to a new beat if the number has changed
        {
            _lastInterval = Mathf.FloorToInt((interval));
           // Debug.Log("CHECK: New Interval: " + _lastInterval + " vs. Emit Beat: " + (time + 1) + " -- " + _noteType); //Log the values for debugging
            //Debug.Log("Interval: " + interval);
            //Check if the note type matches the interval type
            if (_lastInterval == time + 1)
            {
                //Debug.Log("CHECK: Read Type: " + type + " vs. Emitter Type: " + _noteType); //Log the note type for debugging
                if (_noteType == type)
                {
                    //Debug.Log("INVOKE: Spawn"); //Log the values for debugging
                    _trigger.Invoke(); //Invoke the action if it matches
                    proc = true;
                }
            }
        }
        return proc; //Return whether the note was processed
    }

}
