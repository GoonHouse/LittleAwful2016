using UnityEngine;
using System.Collections;

public class BlinkCannonBall : MonoBehaviour {
    public float timeToFlash = 3.0f;
    public float currentTime = 0.0f;

    public Color32 gray = new Color32(118, 118, 118, 255);
    public Color32 lightRed = new Color32(218,  27,  27, 255);

    public Vector3 startScale = new Vector3(0.25f, 0.25f, 1.0f);
    public Vector3 bulgeScale = new Vector3(0.375f, 0.375f, 1.0f);

    public bool alternatingPhase = false;

    public Color32 fromColor;
    public Color32 toColor;
    public Vector3 fromScale;
    public Vector3 toScale;

    public SpriteRenderer sr;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        currentTime -= Time.fixedDeltaTime;

        if( currentTime <= 0.0f ) {
            alternatingPhase = !alternatingPhase;

            if (alternatingPhase) {
                fromColor = lightRed;
                toColor = gray;
                fromScale = bulgeScale;
                toScale = startScale;
            } else {
                fromColor = gray;
                toColor = lightRed;
                fromScale = startScale;
                toScale = bulgeScale;
            }

            // adding because tiny difference in negative deltas
            currentTime += timeToFlash;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.color = Color.Lerp(fromColor, toColor, currentTime / timeToFlash);
        transform.localScale = Vector3.Lerp(fromScale, toScale, currentTime / timeToFlash);
    }
}
