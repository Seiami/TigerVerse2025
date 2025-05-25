using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    private int beatCount = 0;

    private void Update()
    {
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm))); //Gets time elapsed in intervals
            interval.CheckForNewInterval(sampledTime);
        }
    }
}
[System.Serializable]
public class Intervals
{ //Interval = beat length
    [SerializeField] private float _steps; //Intervals (halfnotes, whole, etc?)
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval; //From last funct call

    public float GetIntervalLength(float bpm) //Gets length of current beat
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval) //Checks if we have crossed into a new beat
    {
        //FloortoInt rounds DOWN to the nearest whole number
        if (Mathf.FloorToInt(interval) != _lastInterval) //Check every whole number --> we have passed to a new beat if the number has changed
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke(); //Does the note action
        }
        Debug.Log("Interval: " + _lastInterval);
    }
}