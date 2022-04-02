using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// The speed of the player
    /// </summary>
    [SerializeField, Range (0.1f, 10f)]
    private float speed = 1f;

    /// <summary>
    /// The direction the player is currently moving in
    /// </summary>
    private Vector2 movement;

    /// <summary>
    /// The rigidbody of the player
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    // Update is called once per frame
    /// </summary>
    private void Update()
    {
        // Get the current horizontal input
        this.movement.x = Input.GetAxisRaw("Horizontal");

        // Get the current vertical input
        this.movement.y = Input.GetAxisRaw("Vertical");
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    private void FixedUpdate()
    {
        // Move the player
        // Movement handles the direction
        // Speed handles how fast the player is moving
        // Fixed Delta Time ensures movement is also based on the time since the last frame
        this.rigidBody.MovePosition(this.rigidBody.position + (this.movement * this.speed * Time.fixedDeltaTime));
    }
}
