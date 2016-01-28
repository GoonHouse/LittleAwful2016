using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum RoundStates {
    Uninitialized,
    WaitForPlayerStarted,
    WaitForPlayerFinished,
    RoundStarted,
    RoundFinished,
    RoundWon,
    RoundFailed
}

public class HaggleLogic : MonoBehaviour {
    public float startPrice = 5.0f;
    public float baseTimeLimit = 45.00f;
    public GameObject theLevel;
    public string nextSceneName = "platformer";

    public float price;
    public float time;

    public float basePriceReduction = 0.15f;

    public RoundStates theRoundState = RoundStates.Uninitialized;

    public int totalNumberOfBricks = 0;
    public int numberOfBricks = 0;

    public Text statusText;
    public Text priceText;
    public Text timeText;
    public Button continueButton;
    public Button retryButton;
    public Button leaveButton;

    // Use this for initialization
    void Start () {
        if( SceneManager.GetActiveScene().name == "breakout") {
            EnterGame();
        }
    }

    public void OnLevelWasLoaded(int level) {
        // breakout minigame
        if (level == SceneManager.GetSceneByName("breakout").buildIndex) {
            EnterGame();
        }
    }

    public void EnterGame() {
        //statusText = GameObject.Find("status").GetComponent<Text>();
        priceText = GameObject.Find("price").GetComponent<Text>();
        timeText = GameObject.Find("time").GetComponent<Text>();
        retryButton = GameObject.Find("Retry").GetComponent<Button>();
        retryButton.onClick.AddListener(delegate {
            theRoundState = RoundStates.Uninitialized;
            God.levelTransition.BreakOut(startPrice, baseTimeLimit, theLevel, nextSceneName);
        });
        continueButton = GameObject.Find("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate {
            var p = 0.0f;
            if (God.playerStats.money >= price) {
                p = price;
            }
            God.playerStats.AddItemToInventory(startPrice, price);
            God.levelTransition.Platformer(-p, nextSceneName);
        });

        God.SpawnAt(theLevel, new Vector3(8.5f, 0.5f));

        var bricks = GameObject.FindGameObjectsWithTag("Brick");
        totalNumberOfBricks = bricks.Length;
        numberOfBricks = bricks.Length;

        OnWaitForPlayerStart();
    }

    // we are entering, telling the whole scene to start the player countdown
    public void OnWaitForPlayerStart() {
        theRoundState = RoundStates.WaitForPlayerStarted;
        retryButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        priceText.text = "HAGGLE";
        timeText.text = "CLICK TO DROP PRICES";
    }

    public void OnWaitForPlayerFinish() {
        theRoundState = RoundStates.WaitForPlayerFinished;
        OnRoundStart();
    }

    public void OnRoundStart() {
        theRoundState = RoundStates.RoundStarted;
        price = startPrice;
        time = baseTimeLimit;
        SetMoneyText(price);
        SetTimeText(time);
    }

    public void OnRoundFinish() {
        theRoundState = RoundStates.RoundFinished;

        retryButton.gameObject.SetActive(true);

        if (God.playerStats.money >= price) {
            continueButton.gameObject.SetActive(true);
            OnRoundWin();
        } else {
            continueButton.gameObject.SetActive(false);
            OnRoundFail();
        }
    }

    public void OnRoundWin() {
        theRoundState = RoundStates.RoundWon;
        timeText.text = "ALL SALES FINAL";
    }

    public void OnRoundFail() {
        theRoundState = RoundStates.RoundFailed;
        timeText.text = "INSUFFICIENT FUNDS";
    }

    // Update is called once per frame
    void FixedUpdate () {
        if ( IsRoundActive() ) {
            time -= Time.fixedDeltaTime;
            SetTimeText(time);

            if( time <= 0.0f || numberOfBricks == 0 ) {
                OnRoundFinish();
            }
        }
    }

    public void SetMoneyText(float money) {
        priceText.text = God.FormatMoney(money);
    }

    public void SetTimeText(float time) {
        timeText.text = God.FormatTime(time);
    }

    public float AdjustPrice(float amount) {
        if( IsRoundActive() ) {
            price += amount;

            SetMoneyText(price);
        }
        return price;
    }

    public bool IsRoundActive() {
        return theRoundState.Equals(RoundStates.RoundStarted);
    }
}
