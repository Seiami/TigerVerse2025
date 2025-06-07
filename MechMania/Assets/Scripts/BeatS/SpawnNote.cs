using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//Try to fix using https://www.youtube.com/watch?v=aBzpvUXibw0
//Also try https://www.youtube.com/watch?v=XhghrKi12oA

public class SpawnNote : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject notePrefab;


    public void Spawn()
    {                                                                //Quaternion.identity
        GameObject note = Instantiate(notePrefab, spawnPoint.position, notePrefab.transform.rotation); //Spawns the note at the spawn point (quaternion.identity means no rotation)
        //AddNoteToXRButton(note.GetComponent<NoteObject>()); //Adds the note's OnButtonPress to the selectEntered event of the XR Interactable
        //note.AddComponent<Rigidbody>(); //Adds a rigidbody to the note
        note.transform.SetParent(spawnPoint);
    }
    
}
