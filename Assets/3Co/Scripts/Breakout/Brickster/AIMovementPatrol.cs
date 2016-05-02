using UnityEngine;
using System.Collections;

public class AIMovementPatrol : MonoBehaviour {

    public float patrolProgress = 0.5f;
    public float moveSpeed = 0.5f;
    public float goingDown = 1.0f;

    private Vector2 top;
    private Vector2 bottom;

    // Use this for initialization
    void Start () {
        var rigid = GetComponent<Rigidbody2D>();
        var howFarToGo = gameObject.GetComponent<AbstractPlayer>().howFarToGo;
        top = new Vector2(rigid.position.x, -howFarToGo);
        bottom = new Vector2(rigid.position.x, howFarToGo);
    }
	
	// Update is called once per frame
	void Update () {
        patrolProgress += goingDown * moveSpeed * Time.deltaTime;
        if (patrolProgress >= 1.0f) {
            goingDown = -1.0f;
        } else if (patrolProgress <= 0.0f) {
            goingDown = 1.0f;
        }

        gameObject.GetComponent<AbstractPlayer>().whereIShouldBe = Vector2.Lerp(top, bottom, patrolProgress);
    }
}
