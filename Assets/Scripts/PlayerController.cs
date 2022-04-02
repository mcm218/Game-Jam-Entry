using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int SandResources
    {
        get
        {
            return this._sandResources;
        }
        set
        {
            this._sandResources = value;
            this.sandCounter.text = this._sandResources.ToString();
        }
    }
    /// <summary>
    /// How much sand the player has
    /// </summary>
    public int _sandResources = 10;

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

    public WallController t1WallPrefab;

    public Image progressBarContainer;

    public Image progressBarFill;

    public float actionTimer = 0f;

    public float actionCompleteTime = 1f;

    public bool isPerformingAction = false;

    private Vector2 actionPosition;

    public TextMeshProUGUI sandCounter;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        this.SandResources = this._sandResources;

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
                // Was a wall 

                return;
            }

            // Start placing a T1 wall
            if (this._sandResources > 0)
            {
                this.isPerformingAction = true;
                this.actionPosition = GridController.Instance.GetTileCenter(worldPos);
            }
            // TODO: Hide/Display progress bar based on isPerformingAction
        }

        if (Input.GetMouseButtonUp (0)) 
        {
            this.isPerformingAction = false;
            this.actionTimer = 0f;
            this.progressBarFill.fillAmount = this.actionTimer / this.actionCompleteTime;
        }

        if (this.isPerformingAction)
        {
            this.actionTimer += Time.deltaTime;

            if (this.actionTimer >= this.actionCompleteTime)
            {
                WallController wall = Instantiate<WallController>(this.t1WallPrefab);

                wall.transform.position = this.actionPosition;
                this.isPerformingAction = false;
                this.actionTimer = 0f;
                this.SandResources--;
            }

            this.progressBarFill.fillAmount = this.actionTimer / this.actionCompleteTime;
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
