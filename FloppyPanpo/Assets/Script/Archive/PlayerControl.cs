using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create abstract character class that panpo floppy will inherit from 
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 5.0f;

    // Allows the entire game object to flip
    private bool flipped = false;

    // Single Jump Security Guards
    private bool inAir = false;
    private int oneJump = 0;

    private void OnEnable()
    {
        UpdateHandler.UpdateOccured += ControlPlayerMovement;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccured -= ControlPlayerMovement;
    }

    void ControlPlayerMovement()
    {
        float deltaX = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;
        Vector2 direction = new Vector2(deltaX, 0f);
        if (deltaX != 0) {
            if (deltaX < 0 && !flipped)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y,transform.localScale.z);
                flipped = true;
            }
            else if (deltaX > 0 && flipped)
            {
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
        if (Input.GetButtonDown("Vertical") && !inAir && oneJump < 1) {
            //connect rigidbody in awake then reference 
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            oneJump = 1;
        }
        transform.position += new Vector3(deltaX, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (inAir && (collision.gameObject.name == "Ground" || collision.gameObject.name == "Platform")) {
            Debug.Log("Not in air.");
            oneJump = 0;
            inAir = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (!inAir && (collision.gameObject.name != "Ground" || collision.gameObject.name != "Platform"))
        {
            Debug.Log("In air.");
            oneJump = 1;
            inAir = true;
        }
    }
}
