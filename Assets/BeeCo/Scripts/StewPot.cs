using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StewPot : MonoBehaviour {
    public float timeSpawnDelay = 2.0f;

    public float timeSpawn;
    public int spawnIndex = 0;

    public GameObject spawnPoint;

    public float totalCost;
    public float totalSpent;
    public float totalSaved;

    public Text textTotalCost;
    public Text textCurrentTotalCost;
    public Text textTotalSpent;
    public Text textCurrentTotalSpent;
    public Text textTotalSaved;
    public Text textCurrentTotalSaved;

    // Use this for initialization
    void Start () {
        timeSpawn = timeSpawnDelay;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timeSpawn -= Time.fixedDeltaTime;

        if( timeSpawn < 0.0f && spawnIndex <= God.playerStats.inventory.Count - 1 ) {
            Spawn();
        }
	}

    public void Spawn() {
        timeSpawn = timeSpawnDelay;
        var foodItem = (GameObject)Instantiate(God.playerStats.inventory[spawnIndex], spawnPoint.transform.position, Quaternion.identity);
        spawnIndex++;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        var hitText = (GameObject)Instantiate(God.main.hitText, gameObject.transform.position, Quaternion.identity);
        var basePrice = God.playerStats.basePrices[spawnIndex - 1];
        var pricePaid = God.playerStats.pricesPaid[spawnIndex - 1];

        hitText.GetComponent<FloatTextAway>().SetInvertedMoney(-pricePaid);

        totalSpent += pricePaid;
        SetTextAsMoney(textCurrentTotalSpent, pricePaid, true);
        SetTextAsMoney(textTotalSpent, totalSpent);

        totalCost += basePrice;
        SetTextAsMoney(textCurrentTotalCost, basePrice, true);
        SetTextAsMoney(textTotalCost, totalCost);

        var savedNow = basePrice - pricePaid;
        totalSaved += savedNow;
        SetTextAsMoney(textCurrentTotalSaved, savedNow, true);
        SetTextAsMoney(textTotalSaved, totalSaved);

        Destroy(coll.gameObject, 0.10f);
    }

    public void SetTextAsMoney(Text t, float dollars, bool showSign = false) {
        t.text = God.FormatMoney(dollars);
        t.color = God.ColorOfMoney(dollars);
    }
}
