using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HaggleLogic : MonoBehaviour {
    public float startPrice = 5.0f;
    public float basePriceReduction = 0.15f;
    public float baseTimeLimit = 45.00f;

    public Text priceText;
    public Text timeText;

    public float price;
    public float time;

	// Use this for initialization
	void Start () {
        price = startPrice;
        time = baseTimeLimit;

        priceText.text = "$" + price.ToString("F2");
    }

    void Awake() {
        adjustPrice(0.0f);
    }
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;

        float minutes = Mathf.Floor(time / 60);
        float seconds = (time % 60);

        if( time > 0.0f) {
            timeText.text = minutes.ToString("00") + ":" + seconds.ToString("F5");
        } else {
            timeText.text = "ALL SALES FINAL";
        }
    }

    public float adjustPrice(float amount) {
        if( time > 0.0f) {
            price += amount;

            priceText.text = "$" + price.ToString("F2");
        } else {
            Debug.Log("tried to touch price after-hours!");
        }
        return price;
    }
}
