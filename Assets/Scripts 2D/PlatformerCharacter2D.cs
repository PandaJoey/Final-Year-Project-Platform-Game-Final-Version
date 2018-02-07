using System;
using UnityEngine;

/*
 * Main class for the game used to control and edit what
 * a character can do
 */

namespace UnityStandardAssets._2D {
    public class PlatformerCharacter2D : MonoBehaviour {
        [Range(10.0f, 100.0f)]
        [SerializeField]
        private float maxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [Range(400.0f, 1200.0f)]
        [SerializeField]
        private float jumpForce = 1000f;                 // Amount of force added when the player jumps.
        [Range(0, 1)]
        [SerializeField]
        private float crouchSpeed = 0.50f;               // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField]
        private bool airControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField]
        private LayerMask whatIsGround;                  // A mask determining what is ground to the character.


        private Transform groundCheck;                  // A position marking where to check if the player is grounded.
        const float groundedRadius = 0.2f;              // Radius of the overlap circle to determine if grounded.

        private Transform ceilingCheck;                 // A position marking where to check for ceilings.
        const float ceilingRadius = .01f;               // Radius of the overlap circle to determine if the player can stand up.

        private bool grounded;                          // Whether or not the player is grounded.

        private Animator animator;                      // Reference to the player's animator component.

        private Rigidbody2D rigidBody2D;
        private bool facingRight = true;                // For determining which way the player is currently facing.

        private bool doubleJump = false;                // To determine weather the player has double jumped or not.
        


        // Tells the class what to set to be active on awake.
        private void Awake() {
            // Setting up references.
            groundCheck = transform.Find("GroundCheck");
            ceilingCheck = transform.Find("CeilingCheck");
            animator = GetComponent<Animator>();
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        // Changes functions and varaibles inside on a frame by frame basis.
        private void FixedUpdate() {
            grounded = false;

            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }
            animator.SetBool("Ground", grounded);

            // Set the vertical animation
            animator.SetFloat("vSpeed", rigidBody2D.velocity.y);

            if (grounded) {
                doubleJump = false;
            }


        }

        // Main fucntion to deal with player movement
        public void Move(float move, bool crouch, bool jump) {
            // If crouching, check to see if the character can stand up
            if (!crouch && animator.GetBool("Crouch")) {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround)) {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            animator.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (grounded || airControl) {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move * crouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                animator.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                rigidBody2D.velocity = new Vector2(move * maxSpeed, rigidBody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight) {
                    // ... flip the player.
                    FlipCharacter();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight) {
                    // ... flip the player.
                    FlipCharacter();
                }
            }
            // If the player should jump...
            if ((grounded || !doubleJump) && jump) {
                // Add a vertical force to the player.

                animator.SetBool("Ground", false);
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
                rigidBody2D.AddForce(new Vector2(0f, jumpForce));
                if (!grounded)
                    doubleJump = true;
            }
        }
        // Function to swap the charaters direction.
        private void FlipCharacter() {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        // Used to change the speed scrollbar value which changes the characters speed.
        public void SpeedChange(float speed) {

            if (speed == 0f) {
                maxSpeed = speed + 0.1f * 100f;
            }
            else
                maxSpeed = speed * 100f;

        }

        // Used to change jump height of the character using the scrollbar.
        public void JumpForce(float newForce) {


            if (newForce == 0.0f) {
                jumpForce = newForce + 10f * 100f;
            }
            else if (newForce > 0.05f && newForce < 0.16f) {
                jumpForce = newForce * 11000f;
            }
            else if (newForce > 0.17f && newForce < 0.27f) {
                jumpForce = newForce * 6000f;
            }
            else if (newForce > 0.28f && newForce < 0.38f) {
                jumpForce = newForce * 4333.33f;
            }
            else if (newForce > 0.39f && newForce < 0.5f) {
                jumpForce = newForce * 3500f;
            }
            else if (newForce > 0.39f && newForce < 0.5f) {
                jumpForce = newForce * 3000f;
            }
            else if (newForce > 0.51f && newForce < 0.62f) {
                jumpForce = newForce * 2666.66f;
            }
            else if (newForce > 0.63f && newForce < 0.73f) {
                jumpForce = newForce * 2428.57f;
            }
            else if (newForce > 0.74f && newForce < 0.95f) {
                jumpForce = newForce * 2250f;
            }
            else {
                jumpForce = newForce * 1900f;
            }


        }



        /*
         * to be used in future projects to add special properties to the platforms that
         * were unnessasary in this project. 
        private void OnTriggerEnter2D(Collider2D other) {

        }

        private void OnTriggerExit2D(Collider2D other) {

        }
        */

    }
   

}
