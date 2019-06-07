using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetOnGroundCheck : MonoBehaviour
{
    [HideInInspector] public bool onGround;
    [SerializeField] private Collider2D frontFeet;
    [SerializeField] private Collider2D backFeet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() == frontFeet) {
            Debug.Log("Collider Works");
        }
    }
}
