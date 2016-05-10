using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StewPot : MonoBehaviour {
    public float timeSpawnDelay = 1.0f;

    public float timeSpawn;
    public int spawnIndex = 0;
    public int foodPerSpawn = 5;
    public int foodLeft = 0;

    public GameObject spawnPoint;
    public List<GameObject> thingsToSpawn;

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
        foodLeft = foodPerSpawn * God.playerStats.pricesPaid.Count;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeSpawn -= Time.fixedDeltaTime;

        if( timeSpawn < 0.0f && foodLeft > 0 ) {
            Spawn();
        }
	}

    public void Spawn() {
        timeSpawn = timeSpawnDelay;
        var ind = Random.Range(0, thingsToSpawn.Count - 1);
        God.SpawnAt(thingsToSpawn[ind], spawnPoint.transform.position);
        foodLeft--;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if( (foodLeft % foodPerSpawn) == 0) {
            var hitText = (GameObject)Instantiate(God.main.hitText, gameObject.transform.position, Quaternion.identity);
            var basePrice = God.playerStats.basePrices[spawnIndex];
            var pricePaid = God.playerStats.pricesPaid[spawnIndex];

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

            spawnIndex++;
        }
        
        Destroy(coll.gameObject, 0.10f);
    }

    public void SetTextAsMoney(Text t, float dollars, bool showSign = false) {
        t.text = God.FormatMoney(dollars);
        t.color = God.ColorOfMoney(dollars);
    }
}
