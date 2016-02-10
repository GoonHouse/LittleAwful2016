using UnityEngine;
using System.Collections;

public class SpeedLimit : MonoBehaviour {

    public float ballMinSpeed = 5.0f;
    public float ballMaxSpeed = 25.0f;

    // general
    Rigidbody2D rigid;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Stay within min/max speed.
        if (rigid.velocity.magnitude < ballMinSpeed){
            rigid.velocity = rigid.velocity.normalized * ballMinSpeed;
        } else if (rigid.velocity.magnitude > ballMaxSpeed){
            rigid.velocity = rigid.velocity.normalized * ballMaxSpeed;
        }
    }
}
