using UnityEngine;
using System.Collections;

public class PowerUpItem : MonoBehaviour {
    // should we go towards something
    public bool shouldMove = false;
    public float moveRate = 3.0f;

    // the paddle
    public Paddle paddle;

    // uses
    public float baseUses = 3;
    public float usePerAction = 1;
    public float uses;

	// Use this for initialization
	public virtual void Start () {
        uses = baseUses;
	    if( transform.parent != null) {
            Attach( transform.parent.gameObject );
        }
	}
	
	// Update is called once per frame
	public virtual void FixedUpdate () {
	    if( shouldMove ) {
            var pos = transform.position;
            pos.x -= 3.0f * Time.fixedDeltaTime;
            transform.position = pos;
        }
	}

    public virtual float BallEffect() {
        foreach (GameObject ball in paddle.spawnedBalls) {
            ball.GetComponent<HaggleBall>().FuckOff(1.0f, true);
        }
        return 1.0f;
    }

    public virtual void Suicide() {
        UnDoAction();
        Detach();
        Destroy(gameObject);
    }

    public virtual void ResolveSuicide() {
        if ( uses <= 0.0f ) {
            Suicide();
        }
    }

    public virtual float DoAction() {
        var v = usePerAction * BallEffect();
        uses -= v;
        ResolveSuicide();
        return v;
    }

    public virtual void UnDoAction() {
        ;
        // jack and shit
    }

    // become glued to things
    public virtual void Attach(GameObject attachTo) {
        Debug.Log(gameObject.name + " trying to get on a " + attachTo.name);
        transform.SetParent(attachTo.transform, true);
        paddle = attachTo.GetComponent<Paddle>();

        if( paddle ) {
            if( paddle.powerup ) {
                Debug.Log(gameObject.name + " junking a " + paddle.powerup.gameObject);
                Destroy(paddle.powerup.gameObject);
            }

            paddle.powerup = this;
            var aud = GetComponent<AudioSource>();
            aud.Play();
        }

        shouldMove = false;
        transform.localScale -= new Vector3(0.125f, 0.125f, 0);
    }

    public virtual void Detach() {
        Debug.Log(gameObject.name + " detaching from a " + transform.parent.gameObject.name);
        transform.SetParent(null, true);
        if( paddle ) {
            paddle.powerup = null;
        }
        paddle = null;
        shouldMove = true;
        transform.localScale += new Vector3(0.125f, 0.125f, 0);
    }
}
