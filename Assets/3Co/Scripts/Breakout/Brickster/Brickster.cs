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

    override public void GoWhereIShould() {
        patrolProgress += goingDown * moveSpeed * Time.deltaTime;
        if (patrolProgress >= 1.0f) {
            goingDown = -1.0f;
        } else if (patrolProgress <= 0.0f) {
            goingDown = 1.0f;
        }

        whereIShouldBe = Vector2.Lerp(top, bottom, patrolProgress);

        base.GoWhereIShould();
    }

    // Update is called once per frame
    override public void Update () {
        base.Update();

        brickSpawnTimer -= Time.deltaTime;
        if( brickSpawnTimer <= 0.0f) {
            UseActiveFamiliar(FindAPlaceToSpawn(brick));
            brickSpawnTimer = brickSpawnTimerDelay;
        }
	}
}
