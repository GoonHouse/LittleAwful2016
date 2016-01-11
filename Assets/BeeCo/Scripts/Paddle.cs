using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paddle : MonoBehaviour {
    public float speed = 10.0f;
    public float turnAroundBrake = 2.0f;

    public int numBallsCanSpawn = 1;
    public GameObject haggleBallPrefab;

    private List<GameObject> spawnedBalls;

    // how far the paddle can move relative from its top / bottom
    private float extents = 3.50f;

    // Use this for initialization
    void Start () {
        spawnedBalls = new List<GameObject>();
	}

    private float scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax) {
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
    }

    void UpdatePositionMouse() {
        var rigid = GetComponent<Rigidbody2D>();

        var newPos = new Vector2(rigid.position.x, scale(Input.mousePosition.y, 0, Screen.height, -extents, extents));

        rigid.MovePosition(newPos);
    }

    void UpdatePositionKeyboard() {
        // cancel impulse only once
        if (Input.GetKeyDown("s")) {
            CancelVelocity(true);
        } else if (Input.GetKeyDown("w")) {
            CancelVelocity(false);
        }

        // always add velocity
        if (Input.GetKey("s")) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * -speed);
        } else if (Input.GetKey("w")) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
        }
    }

    public void BallGone(GameObject ball) {
        Debug.Log(ball.name + " reported destruction!");
        spawnedBalls.Remove(ball);
    }

    public void SpawnBall() {
        if( numBallsCanSpawn > 0) {
            var pos = transform.position;
            pos.x += 1.0f;
            var ball = (GameObject)Instantiate(haggleBallPrefab, pos, transform.rotation);
            ball.GetComponent<HaggleBall>().whoMadeMe = gameObject;
            Debug.Log(ball.name + " was spawned!");
            spawnedBalls.Add(ball);
        }
    }

    // Update is called once per frame
    void Update () {
        UpdatePositionMouse();

        if( God.haggleLogic.IsRoundActive()) {
            if( Input.GetKeyDown("space")) {
                SpawnBall();
            }
        }

        if( Input.GetKeyDown("f") ) {
            foreach (GameObject ball in spawnedBalls) {
                ball.GetComponent<HaggleBall>().MagnetizeTowards(gameObject);
            }
        } else if( Input.GetKeyUp("f")) {
            foreach (GameObject ball in spawnedBalls) {
                ball.GetComponent<HaggleBall>().Demagnetize();
            }
        }

    }

    bool CancelVelocity(bool down){
        var rigid = GetComponent<Rigidbody2D>();
        var vel = rigid.velocity;

        if (
            (down && vel.y > 0) || 
            (!down && vel.y < 0)
            )
        {
            Debug.Log("fuck going this way");
            vel.y = vel.y / turnAroundBrake;
            rigid.velocity = vel;
            return true;
        } else {
            return false;
        }
    }
}
