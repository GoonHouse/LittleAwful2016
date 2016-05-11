using UnityEngine;
using System.Collections;

// I don't care who you are or what you do, but having something with this class name is a precious gem, I tell you.
public class ActivityJiggler : MonoBehaviour {
    public bool doSpin = true;
    public float spinRate = 45.0f;
    public float scaleTimeTotal = 1.0f;
    public float scaleFactorMin = 2.0f;
    public float scaleFactorMax = 1.0f;
    public float scaleDir = 1.0f;

    private float spinTime;
    private float scaleTime;
    private Quaternion startRotation;
    private Vector3 startScale;

	// Use this for initialization
	void Start () {
        spinTime = 0.0f;
        scaleTime = 0.0f;
        startRotation = transform.localRotation;
        startScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        if( doSpin ) {
            scaleTime += Time.deltaTime * scaleDir;
            if (scaleTime >= scaleTimeTotal || scaleTime <= 0.0f) {
                // prevent floating point hangups
                if( scaleTime >= scaleTimeTotal) {
                    scaleTime = scaleTimeTotal;
                } else if( scaleTime <= 0.0f) {
                    scaleTime = 0.0f;
                }
                scaleDir *= -1.0f;
            }

            transform.Rotate(0.0f, 0.0f, spinRate);
            transform.localScale = Vector3.Lerp(startScale / scaleFactorMin, startScale * scaleFactorMax, scaleTime / scaleTimeTotal);
        }
	}

    public void StartJiggle() {
        doSpin = true;
        transform.localScale = startScale;
        transform.localRotation = startRotation;
    }

    public void StopJiggle() {
        doSpin = false;
        transform.rotation = startRotation;
        transform.localScale = startScale;
    }
}
