using UnityEngine;
using System.Collections;

/*
    While most people will discourage the use of god objects, this one exists so that
    everything doesn't get trashed between scenes and important things can be found always.
*/
public class God : MonoBehaviour {
    public static God main;

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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
