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

    void OnTriggerEnter2D(Collider2D coll) {
        if( coll.gameObject.tag == "Ball") {
            var bb = coll.gameObject.GetComponent<BaseBall>();
            if (bb != null) {
                if( !selfInflict || (selfInflict && bb.owner != owner)) {
                    coll.gameObject.GetComponent<DoDamage>().Hurt(owner);
                }
            }
        }
    }
}
