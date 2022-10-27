using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [Header("Start and End")]
    public GameObject start;
    public GameObject end;
    public GameObject[] checkpoints;

    [Header("Settings")]
    public float laps = 1;

    [Header("Info")]
    private float currentCheckpoint;
    private float currentLap;
    private bool hasStarted;
    private bool hasFinished;

    private void Start()
    {
        currentCheckpoint = 0;
        currentLap = 1;

        hasStarted = false;
        hasFinished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            GameObject thisCheckpoint = other.gameObject;

            //Check to see if player has started race
            if (thisCheckpoint == start && !hasStarted)
            {
                print("Race Started");
                hasStarted = true;
            }

            //Check to see if player has ended race/lap
            else if (thisCheckpoint == end && hasStarted)
            {
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        hasFinished = true;
                        print(" Race Finished");
                    }
                    else
                    {
                        print("Player did not go through all checkpoints");
                    }
                }
                // Check to start new lap
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        currentLap++;
                        currentCheckpoint = 0;
                        print($"Started Lap {currentLap}");
                    }
                }
                else 
                {
                    print("Player did not go through all checkpoints");
                }
            }

            //Check to see most recent passed checkpoint
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (hasFinished)
                {
                    return;
                }

                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    print("Correct checkpoint");
                    currentCheckpoint++;
                }

                else if (thisCheckpoint == checkpoints[i]&& i != currentCheckpoint)
                {
                    print("Incorrect checkpoint");      
                }
            
            }
        }
    }

}
