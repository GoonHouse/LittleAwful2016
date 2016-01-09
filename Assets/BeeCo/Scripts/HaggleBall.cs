using UnityEngine;
using System.Collections;

public class HaggleBall : MonoBehaviour {
    public float speed = 30.0f;

    Rigidbody2D rigid;
    
	// Use this for initialization
	void Start () {
	    rigid = GetComponent<Rigidbody2D>();
        FuckOff();
    }
    
    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown("space") ){
            FuckOff();
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        if( coll.gameObject.CompareTag("brick") ){
            Destroy(coll.gameObject);
            print("OH BABY IT'S TIME TO SHAVE ME");

            // this.GetComponent<healthScript>().health -= 1;
        }
    }

    void FuckOff(){
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;
        rigid.velocity = v * speed;
    }
}
