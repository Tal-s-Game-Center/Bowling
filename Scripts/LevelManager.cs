using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

public class LevelManager : MonoBehaviour
{
    [Header("Scene Configuration")]
    [SerializeField] private string nextScene; // Name of the next scene
    [SerializeField] private string gameOverScene; // Name of the Game Over scene
    [SerializeField] private string winScene; // Name of the Victory scene

    private GameState currentState;
    private GameObject[] pins;
    private int currentTurn;
    private bool isWaitingForTurnEnd;
    private GameObject ball;

    private int initNumberOfPins;

    private BallController ballController;

    void Start()
    {
        Debug.Log($"LevelManager Start called in scene: {SceneManager.GetActiveScene().name}");
        InitializeLevel();
    }

    void Update()
    {
        if (ballController.GetBallAction() == BallAction.FINISHED)
        {
            Debug.Log("Ball action is FINISHED. Waiting for turn end...");
            isWaitingForTurnEnd = true;
        }

        if (isWaitingForTurnEnd)
        {
            if (IsThrowOver())
            {
                Debug.Log("Throw is over. Checking pin status...");
                if (AllPinsDown())
                {
                    Debug.Log("All pins are down!");
                    ProceedToNextState();
                }
                else
                {
                    Debug.Log("Not all pins are down. Changing turn.");
                    ChangeTurn();
                }
                isWaitingForTurnEnd = false;
            }
        }
    }

    private void InitializeLevel()
    {
        currentState = GameState.LEVEL1;
        currentTurn = 1;
        isWaitingForTurnEnd = false;

        ball = GameObject.Find("Ball");
        if (ball != null)
        {
            ballController = ball.GetComponent<BallController>();
            Debug.Log("Ball and BallController initialized.");
        }
        else
        {
            Debug.LogError("Ball GameObject not found!");
        }

        UpdatePinsArray();
        initNumberOfPins = CountPins();
        Debug.Log($"Initial number of pins: {initNumberOfPins}");
    }

    private void ProceedToNextState()
    {
        if (SceneManager.GetActiveScene().name == winScene)
        {
            Debug.Log("Game Won!");
            LoadScene(winScene);
        }
        else if (!string.IsNullOrEmpty(nextScene))
        {
            Debug.Log($"Loading next scene: {nextScene}");
            LoadScene(nextScene);
        }
        else
        {
            Debug.LogError("Next scene not configured!");
        }
    }

    private void ChangeTurn()
    {
        Debug.Log($"Changing turn. Current turn: {currentTurn}");
        if (currentTurn == 1)
        {
            RemoveDownedPins();
            ballController.ResetBallPos();
            currentTurn++;
            ballController.SetBallAction(BallAction.START);
        }
        else
        {
            Debug.Log("No more turns left. Game over!");
            LoadScene(gameOverScene);
        }
    }

    private void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is null or empty!");
        }
    }

    private void RemoveDownedPins()
    {
        Debug.Log("Removing downed pins...");

        var remainingPins = new System.Collections.Generic.List<GameObject>();

        foreach (GameObject pin in pins)
        {
            PinController pinController = pin.GetComponent<PinController>();
            if (pinController.IsPinDown())
            {
                Destroy(pin); // Destroy the pin
                Debug.Log("Pin destroyed.");
            }
            else
            {
                remainingPins.Add(pin);
            }
        }

        pins = remainingPins.ToArray();
    }

    private int CountPins()
    {
        GameObject pinsObject = GameObject.Find("Pins");

        if (pinsObject != null)
        {
            return pinsObject.transform.childCount;
        }
        else
        {
            Debug.LogError("Pins object not found in the scene.");
            return 0;
        }
    }

    private void UpdatePinsArray()
    {
        Debug.Log("Updating pins array...");
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
            Debug.Log($"Pins array updated with {pinCount} pins.");
        }
        else
        {
            Debug.LogError("Pins object not found in the scene.");
        }
    }

    private bool IsThrowOver()
    {
        Debug.Log("Checking if throw is over...");
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
        Debug.Log("Checking if all pins are down...");
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
