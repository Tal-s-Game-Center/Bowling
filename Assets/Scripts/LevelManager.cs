using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

public class LevelManager : MonoBehaviour
{
    [Header("Scene Configuration")]
    [SerializeField] private string nextScene; // Name of the next scene
    [SerializeField] private string gameOverScene; // Name of the Game Over scene
    [SerializeField] private string winScene; // Name of the Victory scene

    private GameObject[] pins;
    private int currentTurn;
    private bool isWaitingForTurnEnd;
    private GameObject ball;

    private int initNumberOfPins;

    private BallController ballController;

    void Start()
    {
        InitializeLevel();
    }

    void Update()
    {
        // Wait for the ball action to complete before proceeding
        if (ballController.GetBallAction() == BallAction.FINISHED)
        {
            isWaitingForTurnEnd = true;
        }

        if (isWaitingForTurnEnd && IsThrowOver())
        {
            // Handle the end of a throw, depending on pin status
            if (AllPinsDown())
            {
                ProceedToNextState();
            }
            else
            {
                ChangeTurn();
            }
            isWaitingForTurnEnd = false;
        }
    }

    private void InitializeLevel()
    {
        // Set the initial game state and configure references
        currentTurn = 1;
        isWaitingForTurnEnd = false;

        ball = GameObject.Find("Ball");
        if (ball != null)
        {
            ballController = ball.GetComponent<BallController>();
        }

        UpdatePinsArray();
        initNumberOfPins = CountPins();
    }

    private void ProceedToNextState()
    {
        // Transition to the next scene or win state
        if (SceneManager.GetActiveScene().name == winScene)
        {
            LoadScene(winScene);
        }
        else if (!string.IsNullOrEmpty(nextScene))
        {
            LoadScene(nextScene);
        }
    }

    private void ChangeTurn()
    {
        if (currentTurn == 1)
        {
            RemoveDownedPins();
            ballController.ResetBallPos();
            currentTurn++;
            ballController.SetBallAction(BallAction.PLACE);
        }
        else
        {
            LoadScene(gameOverScene);
        }
    }

    private void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    private void RemoveDownedPins()
    {
        // Remove pins that have been knocked down
        var remainingPins = new System.Collections.Generic.List<GameObject>();

        foreach (GameObject pin in pins)
        {
            PinController pinController = pin.GetComponent<PinController>();
            if (!pinController.IsPinDown())
            {
                remainingPins.Add(pin);
            }
            else
            {
                Destroy(pin);
            }
        }

        pins = remainingPins.ToArray();
    }

    private int CountPins()
    {
        GameObject pinsObject = GameObject.Find("Pins");

        return pinsObject != null ? pinsObject.transform.childCount : 0;
    }

    private void UpdatePinsArray()
    {
        // Refresh the array of pin objects
        GameObject pinsObject = GameObject.Find("Pins");

        if (pinsObject != null)
        {
            int pinCount = CountPins();
            GameObject[] tempPinsArray = new GameObject[pinCount];

            for (int i = 0; i < pinCount; i++)
            {
                tempPinsArray[i] = pinsObject.transform.GetChild(i).gameObject;
            }

            pins = tempPinsArray;
        }
    }

    private bool IsThrowOver()
    {
        // Determine if both pins and ball have come to rest
        bool pinsSlowedDown = true;
        foreach (GameObject pin in pins)
        {
            PinController pinController = pin.GetComponent<PinController>();
            if (!pinController.IsSlowedDown())
            {
                pinsSlowedDown = false;
                break;
            }
        }

        bool ballSlowedDown = ballController.IsBallSlowedDown();

        return pinsSlowedDown && ballSlowedDown;
    }

    private bool AllPinsDown()
    {
        // Check if all pins have been knocked down
        foreach (GameObject pin in pins)
        {
            PinController pinController = pin.GetComponent<PinController>();
            if (!pinController.IsPinDown())
            {
                return false;
            }
        }
        return true;
    }
}
