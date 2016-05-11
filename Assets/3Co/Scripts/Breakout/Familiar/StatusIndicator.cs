using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatusIndicator : MonoBehaviour {
    public string state = "default";
    public List<string> stateNames = new List<string>();
    public List<Sprite> sprites = new List<Sprite>();
    public List<Vector3> scales = new List<Vector3>();

    private BaseFamiliar fam;

    // Use this for initialization
    void Start () {
        fam = transform.parent.gameObject.GetComponent<BaseFamiliar>();
	}

    public string GetState() {
        return state;
    }

    public void DetermineState(AbstractPlayer player) {
        if ( !fam.CanShoot() ) {
            Debug.Log("fart");
            SetState("no_uses");
            return;
        }

        if (player.GetActiveFamiliar().GetComponent<BaseFamiliar>() == fam) {
            // we are active
            Debug.Log("meme");
            if (fam.requiresTarget && player.focusedThing == null) {
                Debug.Log("gazorpo");
                SetState("no_target");
                return;
            }
        }

        Debug.Log("yukka");
        SetState("default");
    }

    public void SetState(string statename) {
        var i = stateNames.IndexOf(statename);

        GetComponent<SpriteRenderer>().sprite = sprites[i];
        transform.localScale = scales[i];
    }
}
