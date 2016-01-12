using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LlamaNPC : MonoBehaviour {
    public GameObject textPrefab;
    public Text textObj;

    public float startPrice = 52.69f;
    public float gameTime = 60.0f;

    public bool isTalking = false;
    public int textSaidPos = 0;
    public string textToSay = "Hey buddy you want to buy some :item:?";
    public float textSpeakSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
	    if( isTalking ) {
            Talk();
        } else {

        }
        */
	}

    public void StartTalking() {
        isTalking = true;
        textSaidPos = 0;
        textPrefab.SetActive(true);
        textObj.text = "";

        StartCoroutine("Talk");
        //var dialog = (GameObject)Instantiate(textPrefab, transform.position, transform.rotation);
        //dialog.GetComponent<FloatTextAway>().SetText(whatToSay, true);
    }

    public void ShutUp() {
        isTalking = false;
        textSaidPos = 0;
        textPrefab.SetActive(false);
        textObj.text = "text gone, you shouldn't see this";
        StopCoroutine("Talk");
    }

    IEnumerator Talk() {
        for (int textSaidPos = 0; textSaidPos <= textToSay.Length; textSaidPos++) {
            textObj.text = textToSay.Substring(0, textSaidPos);
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(textSpeakSpeed);
        }
    }
}
