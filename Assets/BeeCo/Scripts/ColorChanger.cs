using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {
	public float timeToFlash = 3.0f;
	public float currentTime = 0.0f;

	public Color32 fromColor;
	public Color32 toColor;

	public SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentTime -= Time.fixedDeltaTime;

		if( currentTime <= 0.0f ) {
			toColor = fromColor;
			fromColor = RandomColor();

			// adding because tiny difference in negative deltas
			currentTime += timeToFlash;
		}

		var sr = GetComponent<SpriteRenderer>();
		sr.color = Color.Lerp(fromColor, toColor, currentTime / timeToFlash);
	
	}

	Color RandomColor(){
		return new Color (Random.value, Random.value, Random.value);
	}

}