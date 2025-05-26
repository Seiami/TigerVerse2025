using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//https://www.youtube.com/watch?v=gIjajeyjRfE
//https://www.youtube.com/watch?v=dtv7mjj_iog

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    static int index = 0; //Static index to keep track of the current note being processed

    private void Start()
    {
        ReadBeatMap.Instance.Read(); //Initialize the beat map
    }

    private void Update()
    {
        index += 1; //Increment the index to process the next note
        if (index >= ReadBeatMap.Instance._count)
        {
            index = 0; //Reset index if it exceeds the number of notes
        }
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm))); //Gets time elapsed in intervals
            interval.CheckForNewInterval(sampledTime, ReadBeatMap.Instance._notes[index][1], ReadBeatMap.Instance._notes[index][0]); //Check if we have crossed into a new interval
        }
    }
}
[System.Serializable]
public class Intervals
{ //Interval = beat length
    [SerializeField] private float _steps; //Intervals (halfnotes, whole, etc?)
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval; //From last funct call
     [SerializeField] private int _noteType; //Type of note

    public float GetIntervalLength(float bpm) //Gets length of current beat
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval, float type, float time) //Checks if we have crossed into a new beat
    {
        //FloortoInt rounds DOWN to the nearest whole number
        if (Mathf.FloorToInt(interval) != _lastInterval) //Check every whole number --> we have passed to a new beat if the number has changed
        {
            _lastInterval = Mathf.FloorToInt(interval);
            Debug.Log("Interval: " + interval);
            CheckForNoteType(_noteType, type, _lastInterval, time);
        }
    }
    
    public void CheckForNoteType(float noteType, float type, float interval, float time)
    {
        //Check if the note type matches the interval type
        if (_noteType == type && interval == time)
        {
            _trigger.Invoke(); //Invoke the action if it matches
        }
    }
}
