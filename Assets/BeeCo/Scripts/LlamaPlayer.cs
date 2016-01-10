using UnityEngine;
using System.Collections;

public class LlamaPlayer : MonoBehaviour {
    private GameObject npcToEngage;

    private void OnTriggerEnter2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null && collideParent.gameObject.CompareTag("NPC")) {
            npcToEngage = collideParent.gameObject;
            npcToEngage.GetComponent<LlamaNPC>().Speak("OY, YOU WANNA CHUMPTY MY FRUMP?");
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null &&
            collideParent.gameObject.CompareTag("NPC") &&
            npcToEngage == collideParent.gameObject )
        {
            // @TODO: tell NPC that I'm gone
            npcToEngage = null;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if( Input.GetKeyDown("f") && npcToEngage != null ) {
            StartMinigame();
        }
	}

    public void StartMinigame() {
        Debug.Log("scream at " + npcToEngage.name);
        var lt = God.main.gameObject.GetComponent<LevelTransiton>();
        var lnpc = npcToEngage.GetComponent<LlamaNPC>();
        lt.BreakOut(lnpc.startPrice, lnpc.gameTime);
    }
}
