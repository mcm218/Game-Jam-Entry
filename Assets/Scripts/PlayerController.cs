using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// How much sand the player has
    /// </summary>
    public int sandResources = 10;

    /// <summary>
    /// How many rocks the player has
    /// </summary>
    public int rockResources = 0;

    /// <summary>
    /// How many seashells the player has
    /// </summary>
    public int seashellResouces = 0;

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
    /// The collider of the player
    /// </summary>
    private CircleCollider2D collider;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        // Getr the player's rigidbody so we can move them
        this.rigidBody = this.GetComponent<Rigidbody2D>();

        // Get the player's collider so we can detect if they touch anything
        this.collider = this.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        // Get the current horizontal input
        this.movement.x = Input.GetAxisRaw("Horizontal");

        // Get the current vertical input
        this.movement.y = Input.GetAxisRaw("Vertical");

        // Did the player left click?
        if (Input.GetMouseButtonDown (0))
        {
            // Get the position of the mouse in the screen
            Vector3 mousePos = Input.mousePosition;

            // Convert the mouse position to a point in the world
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Cast a ray to detect if the player clicked an object with a collider
            RaycastHit2D raycastHit = Physics2D.Raycast(worldPos, Vector2.zero);

            // Was anything clicked?
            if (raycastHit)
            {

            }
        }
    }

    /// <summary>
    /// FixedUpdate is called every fixed framerate frame
    /// </summary>
    private void FixedUpdate()
    {
        // Move the player
        // - Movement handles the direction
        // - Speed handles how fast the player is moving
        // - Fixed Delta Time ensures movement is constrained to the time since the last frame
        this.rigidBody.MovePosition(this.rigidBody.position + (this.movement * this.speed * Time.fixedDeltaTime));
    }
}
