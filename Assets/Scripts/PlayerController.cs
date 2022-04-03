using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum ActionEnum
{
    Building = 0,
    Digging,
    Repairing,
    Upgrading,
    ClearingLitter,
}

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
    [SerializeField, Range(0.1f, 10f)]
    private float speed = 1f;


    [SerializeField, Range(0.1f, 10f)]
    public float clickRange = 1f;


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

    private LitterController litterBeingRemoved;

    private ActionEnum currentAction;

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
        if (Input.GetMouseButtonDown(0))
        {
            // Get the position of the mouse in the screen
            Vector3 mousePos = Input.mousePosition;

            // Convert the mouse position to a point in the world
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            float distance = Vector2.Distance(this.transform.position, worldPos);

            if (Mathf.Abs(distance) <= clickRange)
            {
                // Cast a ray to detect if the player clicked an object with a collider
                RaycastHit2D raycastHit = Physics2D.Raycast(worldPos, Vector2.zero);
                // Was anything clicked?
                if (raycastHit)
                {
                    if (raycastHit.collider.gameObject.name == "DiggingSpot")
                    {
                        this.currentAction = ActionEnum.Digging;
                        this.isPerformingAction = true;
                    }

                    this.litterBeingRemoved = raycastHit.collider.gameObject.GetComponent<LitterController> ();
                    if (this.litterBeingRemoved)
                    {
                        this.currentAction = ActionEnum.ClearingLitter;
                        this.isPerformingAction = true;
                    }
                    // TODO: Starting to repair/upgrade/destroy litter stuff goes here
                }
                else
                {
                    // Are there enough sand resources? If so, start placing a T1 wall
                    if (this.SandResources > 0)
                    {
                        this.currentAction = ActionEnum.Building;
                        this.isPerformingAction = true;
                        this.actionPosition = GridController.Instance.GetTileCenter(worldPos);
                    }
                }
            }

            // TODO: Hide/Display progress bar based on isPerformingAction

        }

        // Is the player performing an action and release the left mouse button?
        if (this.isPerformingAction && Input.GetMouseButtonUp(0))
        {
            // Reset the action progress bar
            this.isPerformingAction = false;
            this.actionTimer = 0f;
            this.progressBarFill.fillAmount = this.actionTimer / this.actionCompleteTime;
        }

        // Is the player performing an action?
        if (this.isPerformingAction)
        {
            // Update the action timer
            this.actionTimer += Time.deltaTime;

            // Has the action completion time been reached?
            if (this.actionTimer >= this.actionCompleteTime)
            {
                switch (this.currentAction)
                {
                    case ActionEnum.Building:
                        // Create a wall object
                        WallController wall = Instantiate<WallController>(this.t1WallPrefab);

                        // Set the wall's position to where the player originally clicked
                        wall.transform.position = this.actionPosition;

                        // Decrement sand resources
                        this.SandResources--;
                        break;
                    case ActionEnum.Digging:
                        this.SandResources++;
                        break;
                    case ActionEnum.Repairing:
                        break;
                    case ActionEnum.Upgrading:
                        break;
                    case ActionEnum.ClearingLitter:
                        Destroy(this.litterBeingRemoved.gameObject);
                        this.litterBeingRemoved = null;
                        break;
                    default:
                        break;
                }

                // Reset the action timer
                this.isPerformingAction = false;
                this.actionTimer = 0f;
            }

            // Update the progress bar
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
        if (this.isPerformingAction == false) { this.rigidBody.MovePosition(this.rigidBody.position + (this.movement * this.speed * Time.fixedDeltaTime)); }
    }
}
