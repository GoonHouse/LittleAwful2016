using UnityEngine;
using System.Collections;

public class LlamaPlayer : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null && collideParent.gameObject.CompareTag("NPC")) {
            var npc = collideParent.gameObject.GetComponent<LlamaNPC>();
            npc.Speak("OY, YOU WANNA CHUMPTY MY FRUMP?");
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
