using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if( Input.GetKeyDown("a") ){
            GetComponent<Rigidbody2D>().velocity = (Vector2.right * speed * -1);
        } else if(Input.GetKeyDown("d")){
            GetComponent<Rigidbody2D>().velocity = (Vector2.right * speed);
        }
    }
}
