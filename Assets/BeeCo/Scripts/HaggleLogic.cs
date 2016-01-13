using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HaggleLogic : MonoBehaviour {
    public float startPrice = 5.0f;
    public float basePriceReduction = 0.15f;
    public float baseTimeLimit = 45.00f;
    public float baseBeginDelay = 3.0f;

    public Text priceText;
    public Text timeText;
    public Button continueButton;

    public float price;
    public float time;

    public bool isActive = false;
    public bool roundEdge = true;
    public float beginDelay = 3.0f;

	// Use this for initialization
	void Start () {
        if( SceneManager.GetActiveScene().name == "breakout") {
            EnterGame();
            RoundStart();
        }
    }

    void Awake() {
        if( priceText ) {
            SetMoneyText(price);
        }
    }

    public void RoundStart() {
        price = startPrice;
        time = baseTimeLimit;
        SetMoneyText(price);
        SetTimeText(time);
        isActive = true;
    }

    public void EnterGame() {
        priceText = GameObject.Find("price").GetComponent<Text>();
        timeText = GameObject.Find("time").GetComponent<Text>();
        continueButton = GameObject.Find("Continue").GetComponent<Button>();
        continueButton.gameObject.SetActive(false);
        continueButton.onClick.AddListener(delegate {
            God.levelTransition.Platformer(price);
        });
    }

    public void OnLevelWasLoaded(int level) {
        // breakout minigame
        if( level == SceneManager.GetSceneByName("breakout").buildIndex ){
            EnterGame();
            RoundStart();
        }
    }

    public void ResetSelf() {
        isActive = false;
        roundEdge = true;
        beginDelay = baseBeginDelay;
    }

    // Update is called once per frame
    void Update () {
        if( isActive && beginDelay > 0) {
            beginDelay -= Time.deltaTime;
            SetTimeText(beginDelay);
            priceText.text = "BEGINNING NEGOTIATION";
        } else if( isActive && beginDelay <= 0) {
            // Hack for when we enter this state.
            if( roundEdge) {
                roundEdge = false;
                SetMoneyText(price);
            }
            time -= Time.deltaTime;

            if (time > 0.0f) {
                SetTimeText(time);
            } else {
                timeText.text = "ALL SALES FINAL";
                if( continueButton) {
                    continueButton.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetMoneyText(float money) {
        priceText.text = money.ToString("C2");
    }

    public void SetTimeText(float time) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        string textForTime = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        timeText.text = textForTime;
    }

    public float adjustPrice(float amount) {
        if( time >= 0.0f) {
            price += amount;

            priceText.text = price.ToString("C2");
        } else {
            Debug.Log("tried to touch the time after-hours");
        }
        return price;
    }

    public bool IsRoundActive() {
        if( time >= 0.0f &&
            isActive && beginDelay <= 0
            ) {
            return true;
        } else {
            return false;
        }
    }
}
