using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class LlamaNPC : MonoBehaviour {
    public GameObject textPrefab;
    public Text textObj;
    public GameObject prevButton;
    public GameObject nextButton;
    public List<AudioClip> talkSounds;

    public GameObject theLevel;
    public string nextSceneName;

    public float startPrice = 52.69f;
    public float gameTime = 60.0f;

    public bool isTalking = false;
    public int textSaidPos = 0;
    public int textCurrentPage = 0;
    public List<string> textsToSay;
    public List<string> postSellTexts;
    public float textSpeakSpeed = 1.0f;

    // this determnies what save data is loaded from the player state
    public int npcIndex = 0;

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

    private void OnTriggerEnter2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null && collideParent.gameObject.CompareTag("Player")) {
            StartTalking();
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null &&
            collideParent.gameObject.CompareTag("Player") ) {
            ShutUp();
        }
    }

    public void Prev() {
        if( textCurrentPage - 1 >= 0 ) {
            StopCoroutine("Talk");
            StartTalking(textCurrentPage - 1);
        }
    }

    public void Next() {
        if( textCurrentPage + 1 <= textsToSay.Count-1 ){
            StopCoroutine("Talk");
            StartTalking(textCurrentPage + 1);
        }
    }

    public void AbsorbSignals() {
        var signals = God.main.holySignals;
        if ( signals.Count > 0) {
            var s = God.main.PopSignal();
            if( s != "" ){
                textsToSay.Insert(textCurrentPage + 1, s);
            }
        }
    }

    public void StartTalking(int page = 0) {
        if ( isTalking ) {
            StopCoroutine("Talk");
        }

        //AbsorbSignals();

        isTalking = true;
        textSaidPos = 0;
        textPrefab.SetActive(true);
        textCurrentPage = page;
        textObj.text = textsToSay[page];
        

        // "I'm an asshole." ~EntranceJew, 2012
        nextButton.SetActive(textCurrentPage < textsToSay.Count-1);
        prevButton.SetActive(textCurrentPage != 0);

        StartCoroutine("Talk");
        //var dialog = (GameObject)Instantiate(textPrefab, transform.position, transform.rotation);
        //dialog.GetComponent<FloatTextAway>().SetText(whatToSay, true);
    }

    public void ShutUp() {
        isTalking = false;
        textSaidPos = 0;
        textPrefab.SetActive(false);
        textCurrentPage = 0;
        textObj.text = "text gone, you shouldn't see this";
        StopCoroutine("Talk");
    }

    public void RandomSound() {
        var n = Random.Range(0, talkSounds.Count - 1);
        var aus = GetComponent<AudioSource>();
        aus.Stop();
        aus.PlayOneShot(talkSounds[n]);
    }

    IEnumerator Talk() {
        for (int textSaidPos = 1; textSaidPos <= textsToSay[textCurrentPage].Length; textSaidPos++) {
            var theText = textsToSay[textCurrentPage].Substring(0, textSaidPos);
            textObj.text = theText;

            // determine if we can pronouce the character
            var theLetter = theText.Substring(textSaidPos - 1, 1);
            var theNumber = (int)System.Text.Encoding.UTF8.GetBytes(theLetter)[0];
            if ( ( theNumber >= 48 && theNumber <= 57  ) || // ascii numbers
                 ( theNumber >= 65 && theNumber <= 90  ) || // ascii caps
                 ( theNumber >= 97 && theNumber <= 122 ) // ascii smalls, the lesser known rapper
                ) {
                RandomSound();
            }
            yield return new WaitForSeconds(textSpeakSpeed);
        }
    }

    public void BeginHaggling() {
        God.levelTransition.BreakOut(startPrice, gameTime, theLevel, nextSceneName);
    }
}
