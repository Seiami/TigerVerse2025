using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_apple : MonoBehaviour
{
    public GameObject Ball;
    public Transform SpawnPoint;

    public void SpawnBall()
    {
        Instantiate(Ball, SpawnPoint);
    }
}
