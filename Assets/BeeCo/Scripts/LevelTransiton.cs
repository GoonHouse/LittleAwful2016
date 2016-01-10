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

    public void Platformer() {
        SceneManager.LoadScene("platformer");
    }

    

    public void BreakOut(float price, float time) {
        SceneManager.LoadScene("breakout");
        var hl = God.main.GetComponent<HaggleLogic>();
        hl.startPrice = price;
        hl.baseTimeLimit = time;
        Debug.Log("NOW BREAKING OUT: " + price.ToString() + ", " + time.ToString());
    }
}
