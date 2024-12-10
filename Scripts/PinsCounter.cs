using TMPro;
using UnityEngine;

public class PinsCounter : MonoBehaviour
{
    public static PinsCounter Instance;
    private TextMeshProUGUI scoreText ;
    public int pinsDown = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Initialize the TextMeshPro component
        scoreText = GetComponent<TextMeshProUGUI>() ;        
    }

    public void IncrementPinCount()
    {
        pinsDown++;
        updateText();
        Debug.Log("Pins down: " + pinsDown);
    }

    public void resetCount()
    {
        pinsDown = 0;
        updateText();
    }

    private void updateText()
    {
        scoreText.SetText(pinsDown.ToString());
    }
}
