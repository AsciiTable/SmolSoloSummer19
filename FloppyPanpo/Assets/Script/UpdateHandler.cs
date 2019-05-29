using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHandler : MonoBehaviour
{
    public delegate void onUpdate();

    public static event onUpdate UpdateOccured;

    private void FixedUpdate()
    {
        if (UpdateOccured != null)
            UpdateOccured();
    }
}
