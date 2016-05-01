using UnityEngine;
using System.Collections;

public class Brickster : AbstractPlayer {

    // how far the paddle can move relative from its top / bottom
    public float patrolProgress = 0.5f;
    public float moveSpeed = 0.5f;
    public float goingDown = 1.0f;

    public float brickSpawnTimerDelay = 1.0f;
    public float brickSpawnTimer;

    private Vector2 top;
    private Vector2 bottom;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        top = new Vector2(rigid.position.x, -howFarToGo);
        bottom = new Vector2(rigid.position.x, howFarToGo);

        brickSpawnTimer = brickSpawnTimerDelay;
    }
	
	// Update is called once per frame
	void Update () {
        patrolProgress += goingDown * moveSpeed * Time.deltaTime;
        if( patrolProgress >= 1.0f) {
            goingDown = -1.0f;
        } else if( patrolProgress <= 0.0f) {
            goingDown = 1.0f;
        }

        GoSomewhere();

        brickSpawnTimer -= Time.deltaTime;
        if( brickSpawnTimer <= 0.0f) {
            SpawnABrick();
            brickSpawnTimer = brickSpawnTimerDelay;
        }
	}

    void GoSomewhere() {
        var newPos = Vector2.Lerp(top, bottom, patrolProgress);

        rigid.MovePosition(newPos);
    }

    void SpawnABrick() {
        var coll = brickZone.GetComponent<BoxCollider2D>().bounds;

        Vector3 pos;
        Vector3 min = coll.min;
        Vector3 max = coll.max;
        var bbounds = brick.GetComponent<BoxCollider2D>().bounds;

        min -= bbounds.extents;
        max -= bbounds.extents;

        pos.x = Random.Range(min.x, max.x);
        pos.y = Random.Range(min.y, max.y);
        pos.z = Random.Range(min.z, max.z);

        GameObject.Instantiate(brick, pos, Quaternion.identity);
    }
}
