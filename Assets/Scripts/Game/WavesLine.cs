using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesLine : MonoBehaviour
{
    public static float speed;

    private List<GameObject> waves;
   

    private float duration;
    private Vector3 startPos, endPos;
    private bool newLineSpawned;

    void Awake()
    {
        // Set new line position to the top of the screen before anything else.
        transform.position = new Vector3(transform.position.x, 22, transform.position.z);
    }

    void Start()
    {
        // Load waves from resources.
        LoadWaves();

        // Spawn wave.
        SpawnLineOfWaves();
        // Get line start position.
        startPos = transform.position;
        // Set line end position.
        endPos = new Vector3(transform.position.x, -22, transform.position.z);
    }

    void Update()
    {
        // If line hasn't reached end position.
        if (transform.position != endPos)
        {
            // If player speed is higher than 0.
            if (speed != 0)
            {
                // How long line will travel to the bottom of the screen.
                duration += Time.deltaTime / (20 - speed);
                // Move line to the bottom of the screen.
                transform.position = Vector3.Lerp(startPos, endPos, duration);

                // How much line has to travel to spawn the new line.
                if (!newLineSpawned && duration > WaveController.instance.waveSpawnRate)
                {
                    // Spawn new line.
                    WaveController.instance.SpawnLine();
                    newLineSpawned = true;
                }
            }
        }
        else
        {
            // Destroy line when it reaches endPos.
            Destroy(gameObject);
        }
    }

    // Load obstacles from resources.
    private void LoadWaves()
    {
        waves = new List<GameObject>();
        Object[] objects = Resources.LoadAll("Waves") as Object[];
        foreach (Object item in objects)
        {
            waves.Add(item as GameObject);
        }
    }

    // Load coin from resources.
    

    // Spawn obstacle in one of five lanes.
    private void SpawnLineOfWaves()
    {
        
        int wavesAmount = 1;

        List<int> availableLanes = new List<int>() { -4, -3, -2, -1, 0, 1, 2, 3, 4 };
        for (int i = 0; i < wavesAmount; i++)
        {
            // Get random lane index.
            int LaneIndex = 0;
            // Spawn obstacle in available line.
            SpawnWave();
            // Remove line, in which new obstacle was spawned, from available lines list.
            availableLanes.RemoveAt(LaneIndex);
        }
    }

    private void SpawnWave()
    {
        int randomWaveIndex = Random.Range(0, waves.Count);
        

        Instantiate(waves[randomWaveIndex], new Vector3(0, 22, 0), Quaternion.identity, transform);
    }




    // Spawn obstacles into the line.

}
