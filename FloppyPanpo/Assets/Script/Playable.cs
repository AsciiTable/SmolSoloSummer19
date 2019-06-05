using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Playable : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;
    public abstract void attack();
    public abstract void special();

    // Allows the entire game object to flip
    private bool flipped = false;

    // Single Jump Security Guards
    bool inAir = false;
    int oneJump = 0;

    public virtual void OnEnable() {
        UpdateHandler.UpdateOccured += control;
    }

    public virtual void OnDisable()
    {
        UpdateHandler.UpdateOccured -= control;
    }

    private void control() {
        float deltaX = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
        Vector2 direction = new Vector2(deltaX, 0f);
        if (deltaX != 0)
        {
            Debug.Log("DeltaX: " + deltaX);
            if (deltaX < 0 && !flipped)
            {
                Debug.Log("Flipping Left");
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                flipped = true;
            }
            else if (deltaX > 0 && flipped)
            {
                Debug.Log("Flipping Right");
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                flipped = false;
            }
            //Create iswalking event create animator controller that responds to event 
            //HOld off for now
            GetComponent<Animator>().SetBool("isWalking", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("isWalking", false);
        }
        //Change to "Jump" reverse changes on vertical input
        //make work w/o one jump, should be revolved around ground detection
        if (Input.GetButtonDown("Vertical") && !inAir && oneJump < 1)
        {
            //connect rigidbody in awake then reference 
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            oneJump = 1;
        }
        transform.position += new Vector3(deltaX, 0, 0);
    }
}
