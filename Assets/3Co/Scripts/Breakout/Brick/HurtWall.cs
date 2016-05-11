using UnityEngine;
using System.Collections;

public class HurtWall : MonoBehaviour {

    public GameObject owner;
    public bool selfInflict = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter2D(Collision2D coll) {
        if( coll.gameObject.tag == "Ball" ) {
            var bb = coll.gameObject.GetComponent<BaseBall>();
            if (bb != null) {
                var dd = bb.gameObject.GetComponent<DoDamage>();
                var impactForce = dd.GetCollisionForce(coll.rigidbody);
                Camera.main.GetComponent<ShakeCamera>().Jostle(impactForce);
                dd.Hurt(owner, impactForce);
            }
        }
    }
}
