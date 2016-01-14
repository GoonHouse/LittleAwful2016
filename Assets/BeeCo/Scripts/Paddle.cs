using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paddle : MonoBehaviour {
    // Combo Stuff
    public float comboTimer = 0.0f;
    public float minComboTimeToExtend = 0.5f;
    public float maxComboTimeToExtend = 1.0f;
    public int baseNumBallsCanSpawn = 1;
    public int combo = 0;
    public int maxCombo = 15;

    // Ball Handling
    public int numBallsCanSpawn;
    public GameObject haggleBallPrefab;
    private List<GameObject> spawnedBalls;

    // Movement Speed
    public float speed = 10.0f;
    public float turnAroundBrake = 2.0f;
    // how far the paddle can move relative from its top / bottom
    private float extents = 3.50f;
    

    // Use this for initialization
    void Start () {
        spawnedBalls = new List<GameObject>();
        numBallsCanSpawn = baseNumBallsCanSpawn;
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

    public void Reset() {
        CancelCombo();

        foreach (GameObject ball in spawnedBalls) {
            Destroy(ball);
        }

        numBallsCanSpawn = baseNumBallsCanSpawn;
    }

    public void BallGone(GameObject ball) {
        spawnedBalls.Remove(ball);
    }

    public void SpawnBall() {
        if( numBallsCanSpawn > 0 ) {
            var pos = transform.position;
            pos.x += 1.0f;
            var ball = (GameObject)Instantiate(haggleBallPrefab, pos, Quaternion.identity);
            ball.GetComponent<HaggleBall>().whoMadeMe = gameObject;
            spawnedBalls.Add(ball);
        }
    }

    // Combo Stuff
    public void CancelCombo() {
        comboTimer = 0.0f;
        combo = 0;
    }

    public void BumpCombo() {
        combo++;
        comboTimer = scale(Mathf.Min(combo, maxCombo), 0, maxCombo, maxComboTimeToExtend, minComboTimeToExtend);
    }

    public float GetComboRatio(bool limit = true) {
        var comboRatio = (float)combo / (float)maxCombo;
        if (limit) {
            comboRatio = Mathf.Min(comboRatio, 1.0f);
        }
        return comboRatio;
    }

    // Update is called once per frame
    void Update () {
        UpdatePositionMouse();

        if( God.haggleLogic.IsRoundActive()) {
            // More balls for the ball god.
            if( Input.GetKeyDown("space")) {
                SpawnBall();
            }

            // Combo logic.
            if (comboTimer > 0) {
                comboTimer -= Time.deltaTime;
            } else {
                CancelCombo();
            }
        }

        // Turn off balls.
        if (Input.GetKeyDown("j")) {
            foreach (GameObject ball in spawnedBalls) {
                var hb = ball.GetComponent<HaggleBall>();
                if ( hb.isGravitized ) {
                    hb.Degravitize();
                } else {
                    hb.Gravitize();
                }
            }
        }

        // Make balls scatter.
        if (Input.GetKeyDown("h")) {
            foreach (GameObject ball in spawnedBalls) {
                ball.GetComponent<HaggleBall>().FuckOff(1.0f, true);
            }
        }

        // Pull towards.
        if ( Input.GetKeyDown("f") ) {
            foreach (GameObject ball in spawnedBalls) {
                ball.GetComponent<HaggleBall>().Magnetize(gameObject, true);
            }
        } else if( Input.GetKeyUp("f")) {
            foreach (GameObject ball in spawnedBalls) {
                ball.GetComponent<HaggleBall>().Demagnetize();
            }
        }

        // Repel.
        if (Input.GetKeyDown("g")) {
            foreach (GameObject ball in spawnedBalls) {
                ball.GetComponent<HaggleBall>().Magnetize(gameObject, false);
            }
        } else if (Input.GetKeyUp("g")) {
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
            vel.y = vel.y / turnAroundBrake;
            rigid.velocity = vel;
            return true;
        } else {
            return false;
        }
    }
}
