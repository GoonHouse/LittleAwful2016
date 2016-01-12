using UnityEngine;
using System.Collections;

public class LlamaPlayer : MonoBehaviour {
    private GameObject npcToEngage;

    private void OnTriggerEnter2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null && collideParent.gameObject.CompareTag("NPC")) {
            Debug.Log("touched a " + collideParent.gameObject.name);
            npcToEngage = collideParent.gameObject;
            npcToEngage.GetComponent<LlamaNPC>().StartTalking();
        }
    }

    private void OnTriggerExit2D(Collider2D coll) {
        var collideParent = coll.gameObject.transform.parent;
        if (collideParent != null &&
            collideParent.gameObject.CompareTag("NPC") &&
            npcToEngage == collideParent.gameObject )
        {
            // @TODO: tell NPC that I'm gone
            npcToEngage.GetComponent<LlamaNPC>().ShutUp();
            npcToEngage = null;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
