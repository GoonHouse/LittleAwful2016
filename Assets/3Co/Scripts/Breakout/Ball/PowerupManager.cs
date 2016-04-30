using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour {

    public GameObject owner;

    public List<IPowerup> powerups = new List<IPowerup>();
    public List<string> powerupNames = new List<string>();

    //@TODO: setup a death signal to politely pop a hole in the containing powerup list

    // Use this for initialization
    void Start () {
        
    }

    void Update() {
        foreach (IPowerup powerup in powerups) {
            powerup.Update(Time.deltaTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
	    foreach(IPowerup powerup in powerups) {
            powerup.FixedUpdate(Time.fixedDeltaTime);
        }
	}

    // methods for handling powerups
    public void CollectPowerup(IPowerup powerup) {
        Debug.Log("COLLECTING POWERUP " + powerup);
        powerupNames.Add(powerup.ToString());
        powerup.OnCollect(gameObject);
        powerups.Add(powerup);
    }
}
