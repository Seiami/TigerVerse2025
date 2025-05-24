using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // public GameObject Ball;
    public Transform SpawnPoint;

    public void SpawnSphere()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = SpawnPoint.position;
        sphere.AddComponent<Rigidbody>();

        // sphereRenderer.material.SetColor("_Color", newSphereColor);
    }
}
