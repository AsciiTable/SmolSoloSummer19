using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panpo : Playable
{
    [SerializeField] private float moveSpeedMultiplier = 1.3f;
    [SerializeField] private float timeSpentRunning = 4.0f;
    private float startTime = 0f;
    protected override void passive() {
        //if()
        walkSpeed = walkSpeed * moveSpeedMultiplier;
    }
    protected override void special() {
    }

    private IEnumerable PassiveTimer() {
    }
}
