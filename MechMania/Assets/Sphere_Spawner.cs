using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere_Spawner : MonoBehaviour
{
    public GameObject mySphere;

    public void SpawnSphere() {
        Instantiate(mySphere);
    }




}
