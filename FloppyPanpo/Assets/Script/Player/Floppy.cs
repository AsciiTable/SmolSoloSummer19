using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floppy : Playable
{
    // SPECIAL: Inspector variables
    [SerializeField] private float specialCD = 10.0f;
    [SerializeField] private float chargeMaxTime = 2f;
    [SerializeField] private float boostSpeed = 10f;

    // SPECIAL: Timer variables to start/reset/end special
    private bool isAvalible = true;
    private bool isCharging = false;
    private float initHold = 0f;
    private float holdTime = 0f;

    protected override void passive()
    {
    }

    // SPECIAL
    protected override void special()
    {
        // Start charging leap
        if (Input.GetMouseButton(0) && isAvalible)
        {
            isAvalible = false;
            isCharging = true;
            initHold = Time.time;
        }
        // If special is on cooldown
        else if (!isAvalible) {
            if (Time.time - initHold > specialCD)
            {
                isAvalible = true;
            }
        }

        // If charging, look for release && leap!
        if (isCharging) {
            if (Input.GetMouseButtonUp(0) || Time.time - initHold >= chargeMaxTime) {
                holdTime = Time.time - initHold;
                if (holdTime > 2) {
                    holdTime = 2f;
                }
                isCharging = false;

                // Leap/Dash/Charge
                if (flipped)
                {
                    rb.velocity = Vector2.left * boostSpeed * (holdTime / chargeMaxTime);
                }
                else
                {
                    rb.velocity = Vector2.right * boostSpeed * (holdTime / chargeMaxTime);
                }
            }
        }
    }
}
