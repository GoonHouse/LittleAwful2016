using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPaddle : MonoBehaviour, IPlayer {
    // Combo Stuff
    public int baseNumBallsCanSpawn = 1;
    

    // Ball Handling
    public int numBallsCanSpawn;
    public List<GameObject> spawnedBalls;
    public GameObject haggleBallPrefab;

    public GameObject focusedBall;
    public int focusedBallIndex;
    public GameObject focusVisualiser;

    // watashi
    public List<BaseFamiliar> familiars;
    public int activeFamiliarIndex = 0;

    // how far the paddle can move relative from its top / bottom
    private float extents = 3.50f;

    // Use this for initialization
    void Start() {
        //spawnedBalls = new List<GameObject>();
        numBallsCanSpawn = baseNumBallsCanSpawn;

        if (focusedBall == null) {
            FocusBallNearest(transform.position);
        }
    }

    public BaseFamiliar GetActiveFamiliar() {
        return familiars[activeFamiliarIndex];
    }

    public BaseBall GetFocusedBall() {
        if( focusedBall != null) {
            return focusedBall.GetComponent<BaseBall>();
        }
        return null;
    }

    void UpdatePositionMouse() {
        var rigid = GetComponent<Rigidbody2D>();

        var newPos = new Vector2(rigid.position.x, God.Scale(Input.mousePosition.y, 0, Screen.height, -extents, extents));

        rigid.MovePosition(newPos);
    }

    public void Reset() {
        GetComponent<Combos>().CancelCombo();

        foreach (GameObject ball in spawnedBalls) {
            Destroy(ball);
        }

        numBallsCanSpawn = baseNumBallsCanSpawn;
    }

    public void BallGone(GameObject ball) {
        spawnedBalls.Remove(ball);
    }

    virtual public bool SpawnBall() {
        if (numBallsCanSpawn > 0) {
            var pos = transform.position;
            pos.x += 1.0f;
            var ball = (GameObject)Instantiate(haggleBallPrefab, pos, Quaternion.identity);
            ball.transform.SetParent(GameObject.Find("BallHell").transform, true);
            ball.GetComponent<PowerupManager>().owner = gameObject;
            ball.GetComponent<BaseBall>().owner = gameObject;
            spawnedBalls.Add(ball);
            return true;
        }
        return false;
    }

    virtual public bool PrimaryButton() {
        return Input.GetMouseButtonDown(0);
    }

    virtual public bool SecondaryButton() {
        return Input.GetMouseButtonDown(1);
    }

    // Update is called once per frame
    void Update() {
        UpdatePositionMouse();

        if(GetActiveFamiliar() != null && GetActiveFamiliar().GetEnergyLeft() > 0.0f) {
            familiars[activeFamiliarIndex].transform.Rotate(0.0f, 0.0f, 2.0f);
        }

        if (PrimaryButton() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            /*
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit && hit.collider != null) {
                Debug.Log("CLICKED ON A: " + hit.transform.gameObject);
                var maybeBall = hit.transform.gameObject;
                if( maybeBall.GetComponent<PowerupManager>() ) {
                    var i = 0;
                    foreach (GameObject ball in spawnedBalls) {
                        if( maybeBall == ball) {
                            focusedBall = maybeBall;
                            focusedBallIndex = i;
                        }
                        i++;
                    }
                }
            }
            */

            FocusBallNearest(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (SecondaryButton()) {
            var af = GetActiveFamiliar();
            var fb = GetFocusedBall();
            Debug.Log(af.ConsumeShot(this, fb));
        }

        if ( focusedBall != null) {
            focusVisualiser.transform.position = focusedBall.transform.position;
        }

        var mouse = Input.GetAxis("Mouse ScrollWheel");
        if (mouse < 0.0f) {
            TargetNudge(1);
        } else if( mouse > 0.0f) {
            TargetNudge(-1);
        }

        if(Input.GetMouseButtonDown(2)) {
            FocusBallNearest(transform.position);
        }
    }

    public void FocusBallNearest(Vector3 position) {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        var goodI = 0;
        var i = 0;
        foreach (GameObject g in spawnedBalls) {
            float dist = Vector3.Distance(g.transform.position, position);
            if (dist < minDist) {
                goodI = i;
                tMin = g.transform;
                minDist = dist;
            }
            i++;
        }
        focusedBall = tMin.gameObject;
        focusedBallIndex = goodI;
    }

    int Mod(int a, int b) {
        return (a % b + b) % b;
    }

    public void TargetNudge(int direction) {
        var numFamiliars = familiars.Count;
        var newPos = (activeFamiliarIndex + direction);
        activeFamiliarIndex = Mod(newPos, numFamiliars);
        //act = paddle.spawnedBalls[paddle.focusedBallIndex];
    }
}
