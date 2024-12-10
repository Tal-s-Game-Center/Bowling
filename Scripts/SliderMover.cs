using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider powerSlider; // Reference to the Slider
    public float speed = 2f;    // Speed of the gauge movement

    private GameObject sliderUI; // The GameObject that contains the slider UI
    private bool isActive = false; // Boolean to control slider activation
    private bool isFilling = true; // Boolean to control filling/unfilling direction

    private void Start()
    {
        // Directly access the Slider component on the same GameObject
        powerSlider = GetComponent<Slider>();

        // Ensure the slider starts at the minimum value
        if (powerSlider != null)
        {
            powerSlider.value = powerSlider.minValue;
        }

        // Access the GameObject the script is attached to for hiding/showing the UI
        sliderUI = gameObject; // Since the script is on the same GameObject as the slider
        ToggleSliderVisibility(false);
    }

    private void Update()
    {
        // Only update the slider if it's active
        if (isActive)
        {
            if (isFilling)
            {
                powerSlider.value += speed * Time.deltaTime;

                if (powerSlider.value >= powerSlider.maxValue)
                {
                    powerSlider.value = powerSlider.maxValue;
                    isFilling = false; // Start un-filling
                }
            }
            else
            {
                powerSlider.value -= speed * Time.deltaTime;

                if (powerSlider.value <= powerSlider.minValue)
                {
                    powerSlider.value = powerSlider.minValue;
                    isFilling = true; // Start filling
                }
            }
        }
    }

    // Function to show or hide the slider
    public void ToggleSliderVisibility(bool isVisible)
    {
        sliderUI.SetActive(isVisible);
    }

    // Function to activate or deactivate the slider filling/unfilling
    public void SetSliderActive(bool active)
    {
        isActive = active;
    }

    // Method to get the current value of the slider
    public float GetValue()
    {
        if (powerSlider != null)
        {
            return powerSlider.value;
        }
        else
        {
            Debug.LogError("PowerSlider is not assigned.");
            return 0f;
        }
    }
}
