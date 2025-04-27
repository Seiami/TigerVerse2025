using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControler : MonoBehaviour
{
    // Variables
    float speed = 7.5f;
    float dydz;
    float dxdz;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 peepee = new Vector3(9.947f,-0.096f,1.011845f);
        Vector3 difs = transform.position - peepee;
        dydz = difs[1]/difs[2];
        dxdz = difs[0]/difs[2];
        transform.eulerAngles = new Vector3(Mathf.Atan(dydz)*(180/3.14f)-2,Mathf.Atan(dxdz)*(180/3.14f),0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-dxdz ,-dydz, -1f)*Time.deltaTime*speed;
        if(transform.position[2]<-36) {
            Destroy(gameObject);
        }
    }
}