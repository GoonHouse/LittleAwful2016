using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransiton : MonoBehaviour {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Platformer(float adjustMoney = 0.0f) {
        SceneManager.LoadScene("platformer");
        God.playerStats.moneyAdjust = adjustMoney;
        Debug.Log("NOW PLATFORMIN': " + adjustMoney);
    }

    public GameObject FindPlayer() {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public void BreakOut(float price, float time) {
        God.playerStats.lastPos = FindPlayer().transform.position;
        God.playerStats.lastCameraPos = Camera.main.transform.position;

        SceneManager.LoadScene("breakout");

        God.haggleLogic.startPrice = price;
        God.haggleLogic.baseTimeLimit = time;
        Debug.Log("NOW BREAKING OUT: " + price.ToString() + ", " + time.ToString());
    }
}
