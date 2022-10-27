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

            //Check to see if player has started race (First Checkpoint)
            if (thisCheckpoint == start && !hasStarted)
            {
                Debug.Log("Race Started");
                hasStarted = true;
            }

            //Check to see if player has ended race/lap (Last Checkpoint)
            else if (thisCheckpoint == end && hasStarted)
            {
                if (currentLap == laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        hasFinished = true;
                        Debug.Log(" Race Finished");
                        thisCheckpoint.SetActive(false);
                    }
                    else
                    {
                        Debug.Log("Player did not go through all checkpoints");
                    }
                }
                //Check to start new lap
                else if (currentLap < laps)
                {
                    if (currentCheckpoint == checkpoints.Length)
                    {
                        currentLap++;
                        currentCheckpoint = 0;
                        print("Started Lap {currentLap}");
                    }
                }
                //If Player passes through Start/End before passing all checkpoints
                else 
                {
                    Debug.Log("Player did not go through all checkpoints");
                }
            }

            //Check to see if passed checkpoint is the correct one
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (hasFinished)
                {
                    return;
                }

                //Correct
                if (thisCheckpoint == checkpoints[i] && i == currentCheckpoint)
                {
                    Debug.Log("Correct checkpoint");
                    currentCheckpoint++;
                    
                    //Takes checkpoints away on final lap
                    if (thisCheckpoint == checkpoints[i] && currentLap == laps)
                    {
                        thisCheckpoint.SetActive(false);
                    }
                        
                }

                //Incorrect
                else if (thisCheckpoint == checkpoints[i]&& i != currentCheckpoint)
                {
                    Debug.Log("Incorrect checkpoint");  
                }
            
            }
        }
    }

}
