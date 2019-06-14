using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Playable : MonoBehaviour
{
    // Variables to be inherited 
    [SerializeField]protected float walkSpeed;
    [SerializeField]protected float jumpMultiplier;
    [SerializeField]protected float fallMultiplier;

    // Methods to be implemented 
    protected abstract void passive();
    protected abstract void special();

    // Allows the entire game object to flip
    private bool flipped = false;
    
    // Component initialization
    protected Rigidbody2D rb;
    private Animator anim;
    private float previousVelocity;
    private float walkSpeedOriginal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        walkSpeedOriginal = walkSpeed;
    }

    public virtual void OnEnable() {
        UpdateHandler.UpdateOccured += control;
        UpdateHandler.UpdateOccured += passive;
        UpdateHandler.UpdateOccured += special;

    }

    public virtual void OnDisable()
    {
        UpdateHandler.UpdateOccured -= control;
        UpdateHandler.UpdateOccured -= passive;
        UpdateHandler.UpdateOccured -= special;
    }

    // Resets any external changes to original speed
    protected virtual void ResetSpeed() {
        walkSpeed = walkSpeedOriginal;
    }

    private void control() {
        // Move left and right
        float deltaX = Input.GetAxis("Horizontal") * walkSpeed * Time.deltaTime;

        // Flip checkers
        if (deltaX != 0)
        {
            if (deltaX < 0 && !flipped)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                flipped = true;
            }
            else if (deltaX > 0 && flipped)
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                flipped = false;
            }
            //Create iswalking event create animator controller that responds to event 
            //Hold off for now
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // Jump
        // If falling, increase falling speed +/ play falling animation
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump")) {
            // If on ground (no change in y axis), then allow the player to jump
            float acceleration = (rb.velocity.y - previousVelocity) / Time.fixedDeltaTime;
            if (rb.velocity.y == 0 && acceleration == 0) {
                rb.velocity = Vector2.up * jumpMultiplier;
                rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
            }
        }

        if (rb.velocity.y != 0)
        {
            anim.SetBool("inAir", true);
        }
        else {
            anim.SetBool("inAir", false);
        }
        transform.position += new Vector3(deltaX, 0, 0);
    }
}
