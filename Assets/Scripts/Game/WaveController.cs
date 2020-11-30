using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{

    [Range(0, 1f)]
    public float waveSpawnRate;

    public static WaveController instance;

    private int lineIndex;

    void Start()
    {
        instance = this;
        // Spawn line when game starts.
        SpawnLine();
    }

    // Used to spawn obstacle line.
    public void SpawnLine()
    {
        // Create new line gameobject.
        GameObject line = new GameObject("WaveLine-" + lineIndex);
        // Make line gameobject child of current gameobject.
        line.transform.parent = transform;
        // Add ObstaclesLine script to the gameobject.
        line.AddComponent<WavesLine>();
       
        // Increase line index.
        lineIndex++;
    }

 


}
