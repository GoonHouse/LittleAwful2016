using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    public float money = 500.0f;
    public Vector3 lastPos = Vector3.zero;
    public Vector3 lastCameraPos = Vector3.zero;
    public float moneyAdjust = 0f;

    public List<float> basePrices;
    public List<float> pricesPaid;
    public List<bool> npcsThatSold;

    // Use this for initialization
    void Start () {
        if (basePrices == null) {
            basePrices = new List<float>();
        }
        if (pricesPaid == null) {
            pricesPaid = new List<float>();
        }
        if (npcsThatSold == null) {
            npcsThatSold = new List<bool>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        // ay
	}

    public void OnLevelWasLoaded(int level) {
        // when we're in platformer again
        if (level == SceneManager.GetSceneByName("platformer").buildIndex) {
            if ( lastPos != Vector3.zero ){
                God.levelTransition.FindPlayer().transform.position = lastPos;
                Camera.main.transform.position = lastCameraPos;
                lastPos = Vector3.zero;
                lastCameraPos = Vector3.zero;
            }

            if ( moneyAdjust != 0.0f ) {
                money += moneyAdjust;
                var player = God.levelTransition.FindPlayer();
                var hitText = (GameObject)Instantiate(God.main.hitText, player.transform.position, Quaternion.identity);
                hitText.GetComponent<FloatTextAway>().SetInvertedMoney(-moneyAdjust);
                moneyAdjust = 0.0f;
            }
        }
    }

    public void AddItemToInventory(float basePrice, float paidPrice) {
        basePrices.Add(basePrice);
        pricesPaid.Add(paidPrice);
    }
}
