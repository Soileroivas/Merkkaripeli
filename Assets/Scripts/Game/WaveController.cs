using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private float duration;
    private Vector3 startPos, endPos;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        
        endPos = new Vector3(transform.position.x, startPos.y - 30, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        speed = ObstaclesLine.speed;

        MoveWave();
    }

    private void MoveWave()
    {
        if (transform.position != endPos)
        {
            // If player speed is higher than 0.
            if (speed != 0)
            {
                // How long wave will travel end position.
                duration += Time.deltaTime / (16 - speed);
                // Move wave to the end position
                transform.position = Vector3.Lerp(startPos, endPos, duration);


            }
        }
        else
        {
            startPos.y = 22;

            //Reset position and duration
            transform.position = startPos;
            duration = 0;
        }
    }
}
