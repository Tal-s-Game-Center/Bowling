using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Enums;

public class BallController : MonoBehaviour
{
    // Input actions for controls
    [SerializeField] InputAction launchKey;
    [SerializeField] InputAction rightKey;
    [SerializeField] InputAction leftKey;

    // Initial position and rotation of the ball
    private Vector3 initPos;
    private Quaternion initRotation;

    // Toggles for visibility of aiming and moving arrows
    private VisibilityToggle aimArrowToggle;
    private VisibilityToggle movingArrowsToggle;

    // Slider controller for power setting
    private SliderController sliderController;

    // Configurable speeds for ball movement and rotation
    [SerializeField] float launchSpeed = 10f;
    [SerializeField] float moveSpeed = 5f; // Movement speed for right and left movement
    [SerializeField] float rotationSpeed = 200f; // Rotation speed for the ball

    // Current action state of the ball
    private BallAction currentAction = BallAction.START;

    // Rigidbody component of the ball
    private Rigidbody ballRB;

    // Velocity threshold to determine when the ball slows down
    [SerializeField] float velocityThreshold = 0.5f;

    void Start()
    {
        // Set default binding for the launch key if none is provided
        if (launchKey.bindings.Count == 0)
        {
            launchKey.AddBinding("<Keyboard>/space");
        }

        // Find and assign arrow toggles
        Transform helpArrowsTransform = transform.Find("HelpArrows");
        Transform aimArrowTransform = helpArrowsTransform.Find("AimArrow");
        Transform movingArrowsTransform = helpArrowsTransform.Find("MovingArrows");
        aimArrowToggle = aimArrowTransform.GetComponent<VisibilityToggle>();
        movingArrowsToggle = movingArrowsTransform.GetComponent<VisibilityToggle>();

        // Find and assign the slider controller
        GameObject canvas = GameObject.Find("UICanvas");
        Transform sliderTransform = canvas.transform.Find("PowerSlider");
        sliderController = sliderTransform.GetComponent<SliderController>();

        // Enable input actions
        leftKey.Enable();
        rightKey.Enable();

        // Initialize ball state
        currentAction = BallAction.START;
        ballRB = GetComponent<Rigidbody>();
        initPos = ballRB.position;
        initRotation = ballRB.rotation;

        // Show moving arrows and enable placement controls
        leftKey.Enable();
        rightKey.Enable();
        currentAction = BallAction.PLACE;
    }

    void OnEnable()
    {
        // Enable the launch key when the object is active
        launchKey.Enable();
    }

    void OnDisable()
    {
        // Disable all keys when the object is inactive
        rightKey.Disable();
        leftKey.Disable();
        launchKey.Disable();
    }

    void Update()
    {
        // Handle input for launching and state transitions
        if (launchKey.WasPressedThisFrame())
        {
            HandleLaunchKeyPress();
        }

        // Handle input for right and left movement or rotation based on the current action
        if (rightKey.IsPressed())
        {
            HandleRightKeyPress();
        }
        if (leftKey.IsPressed())
        {
            HandleLeftKeyPress();
        }
    }

    void FixedUpdate()
    {
        // Launch the ball if in the launch state
        if (currentAction == BallAction.LAUNCH)
        {
            Launch();
        }
    }

    private void HandleLaunchKeyPress()
    {
        switch (currentAction)
        {
            case BallAction.START:
                
                break;

            case BallAction.PLACE:
                // Transition to spin state
                Place();
                currentAction = BallAction.SPIN;
                break;

            case BallAction.SPIN:
                // Lock the angle and transition to set power state
                LockAngle();
                sliderController.ToggleSliderVisibility(true);
                sliderController.SetSliderActive(true);
                currentAction = BallAction.SETPOWER;
                break;

            case BallAction.SETPOWER:
                // Transition to launch state
                sliderController.ToggleSliderVisibility(false);
                currentAction = BallAction.LAUNCH;
                break;

            case BallAction.LAUNCH:
                // Do nothing, launch is handled in FixedUpdate
                break;

            default:
                return;
        }
    }

    private void HandleRightKeyPress()
    {
        if (currentAction == BallAction.PLACE)
        {
            MoveRight();
        }
        else if (currentAction == BallAction.SPIN)
        {
            Rotate(rotationSpeed);
        }
    }

    private void HandleLeftKeyPress()
    {
        if (currentAction == BallAction.PLACE)
        {
            MoveLeft();
        }
        else if (currentAction == BallAction.SPIN)
        {
            Rotate(-rotationSpeed);
        }
    }

    private void Launch()
    {
        // Launch the ball by applying a forward force
        ballRB.AddForce(transform.forward * launchSpeed * sliderController.GetValue(), ForceMode.VelocityChange);
        currentAction = BallAction.FINISHED;
    }

    private void LockAngle()
    {
        // Disable controls and hide the aim arrow
        aimArrowToggle.ToggleVisibility(false);
        leftKey.Disable();
        rightKey.Disable();
    }

    private void Rotate(float angle)
    {
        // Rotate the ball by the specified angle
        transform.Rotate(Vector3.up * angle * Time.deltaTime);
    }

    private void Place()
    {
        // Hide moving arrows and show aim arrow
        movingArrowsToggle.ToggleVisibility(false);
        aimArrowToggle.ToggleVisibility(true);
    }

    private void MoveRight()
    {
        // Move the ball smoothly to the right
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void MoveLeft()
    {
        // Move the ball smoothly to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public bool IsBallSlowedDown()
    {
        // Check if the ball's velocity is below the threshold
        return ballRB.linearVelocity.magnitude < velocityThreshold;
    }

    public BallAction GetBallAction()
    {
        // Return the current action of the ball
        return currentAction;
    }

    public void SetBallAction(BallAction ballAction)
    {
        // Set the current action of the ball
        currentAction = ballAction;
    }

    public void ResetBallPos()
    {
        // Reset the ball to its initial position and state
        ballRB.rotation = initRotation;
        ballRB.position = initPos;
        ballRB.linearVelocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
        // Show moving arrows and enable placement controls
        movingArrowsToggle.ToggleVisibility(true) ;
        leftKey.Enable();
        rightKey.Enable();
    }
}
