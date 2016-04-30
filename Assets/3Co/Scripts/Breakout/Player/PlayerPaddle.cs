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

    // how far the paddle can move relative from its top / bottom
    private float extents = 3.50f;

    // Use this for initialization
    void Start() {
        //spawnedBalls = new List<GameObject>();
        numBallsCanSpawn = baseNumBallsCanSpawn;
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

    public void SpawnBall() {
        if (numBallsCanSpawn > 0) {
            var pos = transform.position;
            pos.x += 1.0f;
            var ball = (GameObject)Instantiate(haggleBallPrefab, pos, Quaternion.identity);
            ball.transform.SetParent(GameObject.Find("BallHell").transform, true);
            ball.GetComponent<PowerupManager>().owner = gameObject;
            spawnedBalls.Add(ball);
        }
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

            Transform tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var goodI = 0;
            var i = 0;
            foreach(GameObject g in spawnedBalls) {
                float dist = Vector3.Distance(g.transform.position, currentPos);
                if( dist < minDist) {
                    goodI = i;
                    tMin = g.transform;
                    minDist = dist;
                }
                i++;
            }
            focusedBall = tMin.gameObject;
            focusedBallIndex = goodI;
        }

        if (SecondaryButton()) {
            SpawnBall();
        }

        if ( focusedBall != null) {
            focusVisualiser.transform.position = focusedBall.transform.position;
        }
    }
}
