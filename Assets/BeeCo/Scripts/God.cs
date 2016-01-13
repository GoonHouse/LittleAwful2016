using UnityEngine;
using System.Collections;

/*
    While most people will discourage the use of god objects, this one exists so that
    everything doesn't get trashed between scenes and important things can be found always.
*/
public class God : MonoBehaviour {
    public static God main;
    public static HaggleLogic haggleLogic;
    public static LevelTransiton levelTransition;
    public static PlayerStats playerStats;

    void Awake() {
        if (main == null) {
            DontDestroyOnLoad(gameObject);
            main = this;
        } else if (main != this) {
            Debug.Log("False prophet detected.");
            DestroyImmediate(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        haggleLogic = GetComponent<HaggleLogic>();
        levelTransition = GetComponent<LevelTransiton>();
        playerStats = GetComponent<PlayerStats>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}