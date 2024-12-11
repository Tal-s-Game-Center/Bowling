using UnityEngine;

public class PinController : MonoBehaviour
{
    private bool isDown = false; // Track if the pin is down
    private Rigidbody pinRB;

    private float velocityThreshold = 0.1f;

    void Start()
    {
        isDown = false; // Initialize the down state
        pinRB = GetComponent<Rigidbody>();

    }

    void Update()
    {
        // Check if the pin is down by analyzing its orientation
        if (!isDown && IsPinDown())
        {
            isDown = true; // Mark the pin as down
            PinsCounter.Instance.IncrementPinCount(); // Update the pin count
            Debug.Log("Pin is down!");
        }
    }


    // Check if the pin's orientation indicates it's down
    public bool IsPinDown()
    {
        // Calculate the angle between the pin's "up" vector and the global "up" vector
        float angle = Vector3.Angle(transform.up, Vector3.up);

        // If the angle exceeds a threshold (e.g., 45°), consider the pin as down
        return angle > 45f;
    }

     public bool IsSlowedDown()
    {
        return pinRB.linearVelocity.magnitude < velocityThreshold;
    }
}
