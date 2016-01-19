using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public float timeTaken = 0.0f;
    public float timeToPos = 3.0f;
    public Vector3 endPos;
    public Vector3 startPos;

    public float distX = 5.0f;
    public float distY = -10.0f;

    public bool shouldBird = false;

    public GameObject speech;
    public Text words;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if( shouldBird ) {
            timeTaken += Time.fixedDeltaTime;
            if (timeTaken <= timeToPos) {
                transform.position = Vector3.Slerp(startPos, endPos, timeTaken / timeToPos);
            } else {
                speech.SetActive(true);
                transform.position = endPos;
            }
        }
	}
}
