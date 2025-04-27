using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControler : MonoBehaviour
{
    // Variables
    float speed = 7.5f;
    float dydz;
    float dxdz;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 peepee = new Vector3(0f,0f,-33.12f);
        Vector3 difs = transform.position - peepee;
        dydz = difs[1]/difs[2];
        dxdz = difs[0]/difs[2];
        transform.eulerAngles = new Vector3(-90f,Mathf.Atan(dxdz)*(180/3.14f),0f);
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