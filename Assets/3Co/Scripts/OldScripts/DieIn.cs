using UnityEngine;
using System.Collections;

public class DieIn : MonoBehaviour {
    public float amount = 3.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, amount);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
