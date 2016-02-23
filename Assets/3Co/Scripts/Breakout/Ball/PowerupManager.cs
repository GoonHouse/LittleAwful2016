using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour {

    public List<BasePowerup> powerups;

	// Use this for initialization
	void Start () {
        powerups.Add(new DebugPowerup());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    foreach(BasePowerup powerup in powerups) {
            powerup.PowerupUpdate(Time.fixedDeltaTime);
        }
	}
}
