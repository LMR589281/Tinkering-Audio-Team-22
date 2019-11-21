using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>The PlayerMovement script captures all of the inputs of the user, and interacts with the character in the game, accordingly. 
    /// 'speed' controls how fast the player is able to move.
    /// 'rb' is a reference to the player's rigidbody2d component.
    /// 'dir' refers to the vector3 movement of the player.
    /// 'playerRadius' is the radius of the player to its collider bounds.
    /// 'playerGrounded' is a boolean variable that keeps track of whether the player is on the ground or not.
    /// </summary>
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float jumpSpeed = 1;

    private Rigidbody2D rb = null;
    private Vector3 dir = new Vector3(0, 0, 0);
    private float playerRadius = 0;
    private bool playerGrounded = true;
    private Animator anim;

    public bool playerHasKey = false;
    public bool canJump = true;

    private void Start()
    {
        /// <summary>Start is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// <para>Assigns the 'rb' variable to the rigidbody2d of the character.
        /// Assigns the 'playerRadius' variable to the bounds of the player's collider.</para>
        /// </summary>
        rb = GetComponent<Rigidbody2D>();
        playerRadius = GetComponent<Collider2D>().bounds.extents.y;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        /// <summary>Update is called every frame.
        /// <para>Checks whether the player is pressing the "jump" key, and whether the boolean variable 'playerGrounded' is true.
        /// If both conditions are true, then an upwards force is applied to the rigidbody.</para>
        /// </summary>
        if (Input.GetButtonDown("Jump") && playerGrounded && canJump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        /// <summary>FixedUpdate is a physics update, that is called every fixed frame-rate frame.
        /// <para>playerGrounded will be set to true if the rigidbody's y-axis velocity is equal to 0, otherwise it will be set to false.
        /// A true value means that the player is on the ground, meaning that they will be able to jump again.
        /// A false value means that the player is currently in the air.</para>
        /// 
        /// <para>Input.GetAxis("Horizontal") returns a value between -1 and 1 - left and right respectively.
        /// This amount is the multiplied by a constant value, called 'speed', and the rigidbody's velocity is changed accordingly.</para>
        /// </summary>
        playerGrounded = rb.velocity.y == 0 ? true : false;
        
        float x = Input.GetAxis("Horizontal");
        dir = new Vector3(x * speed, rb.velocity.y, 0f);
        rb.velocity = dir;

        if (x == 0)
        {
            anim.SetBool("isMoving", false);
        } else
        {
            anim.SetBool("isMoving", true);
        }

        //This if statement is checking the direction in which the player is moving, if they are moving left it rotates the sprite to face left and vis versa
        if (x > 0) //x being greater than 0 would mean it would be 1, this signifies moving right
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z); //Quaternions are used to represent rotations
        }
        else if (x < 0) //x being less than 0 would signify moving left
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
        }
    }

    public void SlowFall()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.1f, 0);
    }
    
    // private void OnCollisionEnter2D(Collision2D coll) { Debug.Log(coll.gameObject.name); }
}
