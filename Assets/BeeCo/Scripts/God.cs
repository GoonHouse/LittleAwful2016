using UnityEngine;
using System.Collections;

/*
    While most people will discourage the use of god objects, this one exists so that
    everything doesn't get trashed between scenes and important things can be found always.
*/
public class God : MonoBehaviour {
    public static God main;

    //How much llama has!
    public static int money;
    //for storing llama's position during breakout games
    public static Vector3 llamaTemporaryPosition;

    void Awake() {
        if (main == null) {
            DontDestroyOnLoad(gameObject);
            main = this;
        } else if (main != this) {
            Debug.Log("False prophet detected.");
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
        money = 500;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}