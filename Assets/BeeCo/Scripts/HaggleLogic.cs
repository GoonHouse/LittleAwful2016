using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HaggleLogic : MonoBehaviour {
    public float startPrice = 5.0f;
    public float basePriceReduction = 0.15f;

    public Text text;

    public float price;

	// Use this for initialization
	void Start () {
        price = startPrice;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float adjustPrice(float amount) {
        price += amount;

        text.text = "$" + price.ToString("F2");
        return price;
    }
}
