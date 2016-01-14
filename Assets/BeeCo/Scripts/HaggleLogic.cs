using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HaggleLogic : MonoBehaviour {
    public float startPrice = 5.0f;
    public float baseTimeLimit = 45.00f;
    public float baseBeginDelay = 3.0f;

    public float price;
    public float time;
    public float beginDelay;

    public float basePriceReduction = 0.15f;

    public enum RoundStates {
        Uninitialized,
        WaitForPlayerStarted,
        WaitForPlayerFinished,
        RoundStarted,
        RoundFinished,
        RoundWon,
        RoundFailed
    }

    public RoundStates theRoundState = RoundStates.Uninitialized;

    public Text priceText;
    public Text timeText;
    public Button continueButton;

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
        priceText = GameObject.Find("price").GetComponent<Text>();
        timeText = GameObject.Find("time").GetComponent<Text>();
        continueButton = GameObject.Find("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate {
            var p = 0.0f;
            if (God.playerStats.money >= price) {
                p = price;
            }
            God.levelTransition.Platformer(-p);
        });
        OnWaitForPlayerStart();
    }

    // we are entering, telling the whole scene to start the player countdown
    public void OnWaitForPlayerStart() {
        theRoundState = RoundStates.WaitForPlayerStarted;
        continueButton.gameObject.SetActive(false);
        priceText.text = "BEGINNING NEGOTIATION";
        beginDelay = baseBeginDelay;
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

        if (God.playerStats.money >= price) {
            OnRoundWin();
        } else {
            OnRoundFail();
        }

        if (continueButton) {
            continueButton.gameObject.SetActive(true);
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
        if (theRoundState == RoundStates.WaitForPlayerStarted) {
            beginDelay -= Time.fixedDeltaTime;
            SetTimeText(beginDelay);
            if (beginDelay <= 0.0f) {
                beginDelay = 0.0f;
                OnWaitForPlayerFinish();
            }
        } else if (theRoundState == RoundStates.RoundStarted) {
            time -= Time.fixedDeltaTime;
            SetTimeText(time);

            if( time <= 0.0f) {
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
        } else {
            Debug.Log("tried to touch the time after-hours");
        }
        return price;
    }

    public bool IsRoundActive() {
        return theRoundState.Equals(RoundStates.RoundStarted);
    }
}
