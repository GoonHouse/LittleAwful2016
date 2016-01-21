using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public float timeTaken = 0.0f;
    public float timeToPos = 0.7f;
    public Vector3 endPos;
    public Vector3 startPos;

    public float timeToLeave = 0.7f;
    public float timeToWait = 2.0f;

    public float distX = 5.0f;
    public float distY = -10.0f;

    public int birdState = 1;

    public GameObject speech;
    public Text words;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if( birdState == 1 ) {
            timeTaken += Time.fixedDeltaTime;
            if (timeTaken <= timeToPos) {
                transform.position = Vector3.Slerp(startPos, endPos, timeTaken / timeToPos);  
            } else {
                speech.SetActive(true);
                transform.position = endPos;
                timeTaken = 0.0f;
                birdState = 2;
                var aud = GetComponent<AudioSource>();
                aud.Play();
            }
        } else if( birdState == 2 ) {
            timeTaken += Time.fixedDeltaTime;
            if (timeTaken <= timeToWait) {
                //transform.position = Vector3.Slerp(endPos, startPos, timeTaken / timeToPos);
            } else {
                speech.SetActive(false);
                timeTaken = 0.0f;
                birdState = 3;
            }
        } else if (birdState == 3) {
            timeTaken += Time.fixedDeltaTime;
            if (timeTaken <= timeToLeave) {
                transform.position = Vector3.Slerp(endPos, startPos, timeTaken / timeToLeave);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
