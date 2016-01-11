using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HaggleLogic : MonoBehaviour {
    public float startPrice = 5.0f;
    public float basePriceReduction = 0.15f;
    public float baseTimeLimit = 45.00f;

    public Text priceText;
    public Text timeText;

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
        if( priceText) {
            priceText.text = "$" + price.ToString("F2");
        }
    }

    public void RoundStart() {
        price = startPrice;
        time = baseTimeLimit;
        priceText.text = "$" + price.ToString("F2");
        isActive = true;
    }

    public void EnterGame() {
        priceText = GameObject.Find("price").GetComponent<Text>();
        timeText = GameObject.Find("time").GetComponent<Text>();
    }

    public void OnLevelWasLoaded(int level) {
        // breakout minigame
        if( level == SceneManager.GetSceneByName("breakout").buildIndex ){
            EnterGame();
            RoundStart();
        }
    }

    // Update is called once per frame
    void Update () {
        if( isActive && beginDelay > 0) {
            beginDelay -= Time.deltaTime;
            priceText.text = (beginDelay % 60).ToString("F5");
            timeText.text = "BEGINNING NEGOTIATION";
        } else if( isActive && beginDelay <= 0) {
            // Hack for when we enter this state.
            if( roundEdge) {
                roundEdge = false;
                priceText.text = "$" + price.ToString("F2");
            }
            time -= Time.deltaTime;

            float minutes = Mathf.Floor(time / 60);
            float seconds = (time % 60);

            if (time > 0.0f) {
                timeText.text = minutes.ToString("00") + ":" + seconds.ToString("F5");
            } else {
                timeText.text = "ALL SALES FINAL";
            }
        }
    }

    public float adjustPrice(float amount) {
        if( time >= 0.0f) {
            price += amount;

            priceText.text = "$" + price.ToString("F2");
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
