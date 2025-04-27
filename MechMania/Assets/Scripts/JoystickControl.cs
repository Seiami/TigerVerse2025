using System;
using UnityEngine;
using TMPro;

public class JoystickControl : MonoBehaviour
{
    public Transform topOfJoystick;
    public TextMeshProUGUI outputText;

    [SerializeField]
    private float forwardBackwardTilt = 0;
    [SerializeField]
    private float sideToSideTilt = 0;

    // Called once per frame
    void Update()
    {
    forwardBackwardTilt = topOfJoystick.rotation.eulerAngles.x;
    if(forwardBackwardTilt<355 && forwardBackwardTilt>290)
    {
        forwardBackwardTilt = Math.Abs(forwardBackwardTilt - 360);
        Debug.Log("Backward" + forwardBackwardTilt);
        outputText.text = forwardBackwardTilt.ToString("F0");
    // move something using forward Backward Tilt as speed
    }
    else if (forwardBackwardTilt > 5 && forwardBackwardTilt <74)
    {
        Debug.Log("Forward" + forwardBackwardTilt);
        outputText.text = forwardBackwardTilt.ToString("F0");

    }

    sideToSideTilt = topOfJoystick.rotation.eulerAngles.z;
    if(sideToSideTilt<355 && sideToSideTilt>290)
    {
        sideToSideTilt = Math.Abs(sideToSideTilt - 360);
        Debug.Log("Right" + sideToSideTilt);
        outputText.text = sideToSideTilt.ToString("F0");
    }
    else if (sideToSideTilt > 5 && sideToSideTilt <74)
    {
        Debug.Log("left" + sideToSideTilt);
        outputText.text = sideToSideTilt.ToString("F0");
    }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerHand"))
        {
            transform.LookAt(other.transform.position, transform.up);
        }
    }

}
