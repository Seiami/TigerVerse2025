using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Try to fix using https://www.youtube.com/watch?v=aBzpvUXibw0
//Also try https://www.youtube.com/watch?v=XhghrKi12oA

public class SpawnNote : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject notePrefab;


    public void Spawn()
    {                                                          //Quaternion.identity
        var note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity); //Spawns the note at the spawn point (quaternion.identity means no rotation)
        note.AddComponent<Rigidbody>(); //Adds a rigidbody to the note
        note.transform.SetParent(spawnPoint);

    }
}
