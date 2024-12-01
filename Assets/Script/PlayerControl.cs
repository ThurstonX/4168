using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Public variables for movement speed and jump force
    Rigidbody body;
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public AudioSource jumpSound;
    
    // Check if the character is on the ground
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, groundLayer);
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Character Rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontalInput, 0);

        // Character forward and backward
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.velocity = new Vector3(body.velocity.x, jumpForce, body.velocity.z);
            jumpSound.Play();
        }

    }
}
