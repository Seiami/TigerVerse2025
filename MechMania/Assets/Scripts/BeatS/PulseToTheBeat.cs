using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://www.youtube.com/watch?v=gIjajeyjRfE
public class PulseToTheBeat : MonoBehaviour
{

    [SerializeField] bool _useTestBeat;
    [SerializeField] float _pulseSize = 1.15f;
    [SerializeField] float _returnSpeed = 6f;
    private Vector3 _startSize;

    // Start is called before the first frame update
    private void Start()
    {
        _startSize = transform.localScale;
        if (_useTestBeat)
        {
            StartCoroutine(TestBeat());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _startSize, Time.deltaTime * _returnSpeed);
    }

    public void Pulse()
    {
        transform.localScale = _startSize * _pulseSize;
    }

    IEnumerator TestBeat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}
