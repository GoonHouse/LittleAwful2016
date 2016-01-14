using UnityEngine;
using System.Collections;

public class PowerUpItem : MonoBehaviour {
    // should we go towards something
    public bool shouldMove = false;
    public float moveRate = 1.0f;

    // the paddle
    public Paddle paddle;

    // uses
    public int baseUses = 3;
    public int uses;

	// Use this for initialization
	void Start () {
        uses = baseUses;
	    if( transform.parent != null) {
            Attach( transform.parent.gameObject );
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if( shouldMove ) {
            var pos = transform.position;
            pos.x -= moveRate * Time.fixedDeltaTime;
            transform.position = pos;
        }
	}

    public bool DoAction() {
        foreach( GameObject ball in paddle.spawnedBalls ){
            ball.GetComponent<HaggleBall>().FuckOff(1.0f, true);
        }

        uses--;

        if (uses <= 0) {
            Detach();
            DestroyImmediate(gameObject);
        }
        return true;
    }

    // become glued to things
    public void Attach(GameObject attachTo) {
        transform.SetParent(attachTo.transform, true);
        paddle = attachTo.GetComponent<Paddle>();
        if( paddle ) {
            paddle.powerup = this;
        }
        shouldMove = false;
        transform.localScale -= new Vector3(0.125f, 0.125f, 0);
    }

    public void Detach() {
        transform.SetParent(null, true);
        if( paddle) {
            paddle.powerup = null;
        }
        paddle = null;
        shouldMove = true;
        transform.localScale += new Vector3(0.125f, 0.125f, 0);
    }
}
