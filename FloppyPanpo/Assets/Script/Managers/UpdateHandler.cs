using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHandler : MonoBehaviour
{
    public delegate void onUpdate();

    public static event onUpdate UpdateOccured;

    private void Update()
    {
        if (UpdateOccured != null)
            UpdateOccured();
    }
}
