using UnityEngine;
using System.Collections;

public class PowerUpItem : MonoBehaviour {
    // should we go towards something
    public bool shouldMove = false;
    public float moveRate = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if( shouldMove) {
            var pos = transform.position;
            pos.x -= moveRate * Time.fixedDeltaTime;
            transform.position = pos;
        }
	}

    public void Born() {
        // not to be mistaken for bjork, nor bjorn
        shouldMove = true;
    }
}
