using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerupManager : MonoBehaviour {

    public List<IPowerup> powerups = new List<IPowerup>();

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    foreach(IPowerup powerup in powerups) {
            powerup.Update(Time.fixedDeltaTime);
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        var go = coll.gameObject;
        if (go.CompareTag("Powerup")) {
            var bp = go.GetComponent<BestowPowerup>();
            var g = (IPowerup)System.Activator.CreateInstance(
                System.Reflection.Assembly.GetExecutingAssembly().FullName,
                bp.nameOfClass
            ).Unwrap();
            g.OnCollect(gameObject);
            powerups.Add(g);
            Destroy(go);
        }
    }
}
