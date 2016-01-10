using UnityEngine;
using System.Collections;

public class LlamaNPC : MonoBehaviour {
    public GameObject textPrefab;

    public float startPrice = 52.69f;
    public float gameTime = 60.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Speak(string whatToSay) {
        var dialog = (GameObject)Instantiate(textPrefab, transform.position, transform.rotation);
        dialog.GetComponent<FloatTextAway>().SetText(whatToSay, true);
    }
}
