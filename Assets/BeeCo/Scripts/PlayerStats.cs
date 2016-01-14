using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    public float money = 500.0f;
    public Vector3 lastPos = Vector3.zero;
    public Vector3 lastCameraPos = Vector3.zero;
    public float moneyAdjust = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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
}
