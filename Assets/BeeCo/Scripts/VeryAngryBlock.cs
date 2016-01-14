using UnityEngine;
using System.Collections;

public class VeryAngryBlock : MonoBehaviour {
    public float forceToAdd = 400.0f;
    Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 v = Quaternion.AngleAxis(90.0f, Vector3.forward) * -Vector3.up;
        v.x *= forceToAdd;
        rigid.AddForce(v);
	}
}
