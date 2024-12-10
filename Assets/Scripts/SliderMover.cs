using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [Header("Slider Settings")]
    [SerializeField] private float speed = 2f; // Speed of the gauge movement

    private Slider powerSlider; // Reference to the Slider component
    private bool isActive = false; // Controls whether the slider is active
    private bool isFilling = true; // Determines if the slider is filling or un-filling

    void Start()
    {
        // Get the Slider component attached to this GameObject
        powerSlider = GetComponent<Slider>();
        if (powerSlider == null)
        {
            Debug.LogError("Slider component not found on the GameObject.");
            return;
        }

        // Initialize the slider value to its minimum
        powerSlider.value = powerSlider.minValue;

        // Hide the slider UI at the start
        ToggleSliderVisibility(false);
    }

    void Update()
    {
        if (!isActive || powerSlider == null) return;

        // Update the slider value based on the current direction (filling/unfilling)
        powerSlider.value += (isFilling ? 1 : -1) * speed * Time.deltaTime;

        // Reverse direction when reaching min or max value
        if (powerSlider.value >= powerSlider.maxValue)
        {
            powerSlider.value = powerSlider.maxValue;
            isFilling = false; // Start decreasing
        }
        else if (powerSlider.value <= powerSlider.minValue)
        {
            powerSlider.value = powerSlider.minValue;
            isFilling = true; // Start increasing
        }
    }

    /// Shows or hides the slider UI.
    public void ToggleSliderVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    /// Activates or deactivates the slider's automatic filling/unfilling.
    public void SetSliderActive(bool active)
    {
        isActive = active;
    }

    /// Gets the current value of the slider.
    public float GetValue()
    {
        if (powerSlider == null)
        {
            Debug.LogError("Slider component is not assigned.");
            return 0f;
        }

        return powerSlider.value;
    }
}
