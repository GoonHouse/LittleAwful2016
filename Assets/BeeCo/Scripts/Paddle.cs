using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    public float speed = 10.0f;
    public float turnAroundBrake = 2.0f;

    public GameObject haggleBall;

    // how far the paddle can move relative from its top / bottom
    private float extents = 3.50f;

    // Use this for initialization
    void Start () {
	
	}

    private float scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax) {
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
    }

    // Update is called once per frame
    void Update () {
        var rigid = GetComponent<Rigidbody2D>();

        var newPos = new Vector2(rigid.position.x, scale(Input.mousePosition.y, 0, Screen.height, -extents, extents));

        rigid.MovePosition(newPos);

        /*
        // cancel impulse only once
        if (Input.GetKeyDown("s")){
            CancelVelocity(true);
        } else if (Input.GetKeyDown("w")){
            CancelVelocity(false);
        }

        // always add velocity
        if ( Input.GetKey("s") ){
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * -speed);
        } else if( Input.GetKey("w") ){
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * speed);
        }
        */
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
