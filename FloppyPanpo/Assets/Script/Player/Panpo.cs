﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panpo : Playable
{
    // PASSIVE: Inspector variables
    [SerializeField] private float moveSpeedMultiplier = 1.3f;
    [SerializeField] private float timeSpentRunning = 4.0f;
    [SerializeField] private float timeWithIncSpeed = 2.0f;
    
    // PASSIVE: Timer variables to start/reset/end passive
    private float startTimeRunning = 0f;
    private bool  timerStarted = false;
    private float startTimeSpeed = 0f;

    // PASSIVE: Calcuate increased speed ONCE
    private bool awakePassive = false;
    private float increasedMovement = 0f;


    // SPECIAL: Inspector variables
    [SerializeField] private float specialDuration = 2.5f;
    [SerializeField] private float specialCD = 10.0f;

    // SPECIAL: Timer variables to start/reset/end special
    private bool isAvalible = true;
    private float duration = 0f;

    // PASSIVE
    protected override void passive() {
        // If the game just began, calculate the value of the increased move speed
        if (!awakePassive) {
            increasedMovement = walkSpeed * moveSpeedMultiplier;
            awakePassive = true;
        }

        // Get the initial time where 'a' or 'd' is initially pressed
        if (Input.GetButtonDown("Horizontal")) {
            startTimeRunning = Time.time;
        }

        // If 'a' or 'd' is held down for longer than the time required to trigger the passive
        if (Input.GetButton("Horizontal") && Time.time - startTimeRunning > timeSpentRunning){
            // Start the timer to limit the amount of time Panpo's speed is boosted for
            if (!timerStarted){
                startTimeSpeed = Time.time;
                timerStarted = true;
                walkSpeed = increasedMovement;
            }

            // If the time limit is exceeded, reset the walking speed and timers
            if (Time.time - startTimeSpeed > timeWithIncSpeed){
                ResetSpeed();
                timerStarted = false;
                startTimeRunning = Time.time;
            }
        }
        else { // if moving stopped or direction is switched, reset the timer/speed
            ResetSpeed();
            timerStarted = false;
        }
    }

    // SPECIAL
    protected override void special() {
        // If player activates the special while it is not on cooldown
        if (Input.GetMouseButtonDown(0) && isAvalible){
            isAvalible = false;
            duration = Time.time;
        }
        // If the special is on cooldown
        else if (!isAvalible) {
            if (Time.time - duration > specialCD) {
                isAvalible = true;
            }
        }
        //Unlimited jump!
        if (Input.GetButtonDown("Jump")) {
            if (!isAvalible && Time.time - duration < specialDuration) {
                rb.velocity = Vector2.up * jumpMultiplier;
                rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }
}
