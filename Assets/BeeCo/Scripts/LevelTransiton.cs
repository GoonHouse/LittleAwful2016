using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransiton : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Platformer(float moneyGot) {
        SceneManager.LoadScene("platformer");
        if (God.llamaTemporaryPosition != Vector3.zero) {
            GameObject.FindGameObjectWithTag("Llama").transform.position = God.llamaTemporaryPosition;
            God.llamaTemporaryPosition = Vector3.zero;
        }
        God.money += moneyGot;
        Debug.Log("NOW PLATFORMIN': " + moneyGot);
    }

    

    public void BreakOut(float price, float time) {
        God.llamaTemporaryPosition = GameObject.FindGameObjectWithTag("Llama").transform.position;
        SceneManager.LoadScene("breakout");
        var hl = God.main.GetComponent<HaggleLogic>();
        hl.startPrice = price;
        hl.baseTimeLimit = time;
        Debug.Log("NOW BREAKING OUT: " + price.ToString() + ", " + time.ToString());
    }
}
