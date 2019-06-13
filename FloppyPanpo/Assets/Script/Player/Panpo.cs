using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panpo : Playable
{
    [SerializeField] private float moveSpeedMultiplier = 1.3f;
    [SerializeField] private float timeSpentRunning = 4.0f;
    [SerializeField] private float timeWithIncSpeed = 2.0f;

    private float startTimeRunning = 0f;
    private bool  timerStarted = false;
    private float startTimeSpeed = 0f;

    private bool awake = false;
    private float increasedMovement = 0f;

    protected override void passive() {
        Debug.Log("Move Speed: " + walkSpeed);
        if (!awake) {
            increasedMovement = walkSpeed * moveSpeedMultiplier;
            awake = true;
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            startTimeRunning = Time.time;
        }

        if (Input.GetButton("Horizontal") && Time.time - startTimeRunning > timeSpentRunning)
        {
            if (!timerStarted)
            {
                startTimeSpeed = Time.time;
                timerStarted = true;
            }

            if (Time.time - startTimeSpeed < timeWithIncSpeed)
            {
                //Debug.Log("Time Sped up: " + (Time.time - startTimeSpeed));
                walkSpeed = increasedMovement;
            }
            else
            {
                Debug.Log("Reached inner Reset");
                ResetSpeed();
                timerStarted = false;
                startTimeRunning = Time.time;
            }
        }
        else {
            ResetSpeed();
            timerStarted = false;
        }
    }
    protected override void special() {
    }

}
