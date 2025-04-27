using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    // Variables
    float speed = 7.5f;
    // float dydz;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 peepee = new Vector3(0f,6.26f,-33.12f);
        Vector3 difs = transform.position - peepee;
        // dydz = difs[1];
        transform.eulerAngles = new Vector3(Mathf.Atan(difs[1]/difs[2])*(180/3.14f)-2,Mathf.Atan(difs[0]/difs[2])*(180/3.14f),0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-0.17f*(Mathf.Abs(transform.position[0])/transform.position[0]) ,0f,-1f)*Time.deltaTime*speed;
        if(transform.position[2]<-36) {
            Destroy(gameObject);
        }
    }
}