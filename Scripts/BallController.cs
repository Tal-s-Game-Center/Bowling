using System.Threading;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using Enums ;
public class BallController : MonoBehaviour
{
    [SerializeField] InputAction launchKey;
    [SerializeField] InputAction rightKey;
    [SerializeField] InputAction leftKey;

    private Vector3 initPos ;
    private Quaternion initRotation ;
    private VisibilityToggle aimArrowToggle ;
    private VisibilityToggle movingArrowsToggle ;

    private SliderController sliderController ;

    [SerializeField] float launchSpeed = 10f;
    [SerializeField] float moveSpeed = 5f; // Movement speed for right and left movement
    [SerializeField] float rotationSpeed = 200f; // Rotation speed for the ball

    private BallAction currentAction = BallAction.START;
    private Rigidbody ballRB ;
    
    [SerializeField] float velocityThreshold = 0.5f;
    
    void Start()
    {
        if (launchKey.bindings.Count == 0)
        {
            launchKey.AddBinding("<Keyboard>/space");
        }
        Transform helpArrowsTransform = transform.Find("HelpArrows");
        Transform aimArrowTransform = helpArrowsTransform.Find("AimArrow");
        Transform movingArrowsTransform = helpArrowsTransform.Find("MovingArrows");

        aimArrowToggle = aimArrowTransform.GetComponent<VisibilityToggle>() ;
        movingArrowsToggle = movingArrowsTransform.GetComponent<VisibilityToggle>() ;

        GameObject canvas = GameObject.Find("UICanvas") ;
        Transform sliderTransform = canvas.transform.Find("PowerSlider") ;
        sliderController = sliderTransform.GetComponent<SliderController>() ;
        
        leftKey.Enable() ;
        rightKey.Enable() ;
        currentAction = BallAction.START ;
        ballRB = GetComponent<Rigidbody>() ;
        initPos = ballRB.position ;
        initRotation = ballRB.rotation ;
    }

    void OnEnable()
    {
        launchKey.Enable();
    }

    void OnDisable()
    {
        rightKey.Disable() ;
        leftKey.Disable() ;
        launchKey.Disable();
    }

    void Update()
    {
        if (launchKey.WasPressedThisFrame())
        {
            switch (currentAction)
            {
                case BallAction.START:
                    movingArrowsToggle.ToggleVisibility(true) ;
                    leftKey.Enable() ;
                    rightKey.Enable() ;
                    currentAction = BallAction.PLACE ;
                    break ;
                case BallAction.PLACE:
                    currentAction = BallAction.SPIN;
                    place();
                    break;
                case BallAction.SPIN:
                    lockAngle();
                    currentAction = BallAction.SETPOWER;
                    sliderController.ToggleSliderVisibility(true) ;
                    sliderController.SetSliderActive(true) ;
                    break;
                case BallAction.SETPOWER:
                    currentAction = BallAction.LAUNCH ;
                    sliderController.ToggleSliderVisibility(false) ;
                    break ;
                case BallAction.LAUNCH:
                    break;
                default:
                    return;
            }
        }
        if (rightKey.IsPressed())
        {
            if (currentAction == BallAction.PLACE)
            {
                moveRight();
            }else if(currentAction == BallAction.SPIN)
            {
                rotate(10) ;
            }
        }
        if (leftKey.IsPressed())
        {
            if (currentAction == BallAction.PLACE)
            {
                moveLeft();
            }else if(currentAction == BallAction.SPIN){
                rotate(-10) ;
            }        
        }
    }

    void FixedUpdate()
    {
        if (currentAction == BallAction.LAUNCH)
        {
            launch();
        }
    }

    private void launch()
    {
        // Launch the ball by moving it forward
        ballRB.AddForce(transform.forward * launchSpeed * sliderController.GetValue(), ForceMode.VelocityChange) ;
        currentAction = BallAction.FINISHED ;
    }

    private void lockAngle(){
        aimArrowToggle.ToggleVisibility(false) ;
        leftKey.Disable();
        rightKey.Disable();
    }
    private void rotate(float angle)
    {
        transform.Rotate(Vector3.up * angle * Time.deltaTime) ;
    }

    private void place()
    {
        movingArrowsToggle.ToggleVisibility(false) ;
        aimArrowToggle.ToggleVisibility(true) ;
    }

    private void moveRight()
    {
        // Move the ball smoothly to the right by changing its position
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void moveLeft()
    {
        // Move the ball smoothly to the left by changing its position
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
    public bool IsBallSlowedDown()
    {
        // If the ball's velocity is smaller than the threshold, it's considered stopped
        return ballRB.linearVelocity.magnitude < velocityThreshold;
    }

    public BallAction GetBallAction(){
        return currentAction ;
    }
    public void SetBallAction(BallAction ballAction){
        currentAction = ballAction ;
    }

    public void ResetBallPos(){
        ballRB.rotation = initRotation ;
        ballRB.position = initPos ;
        ballRB.linearVelocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
    }
}
