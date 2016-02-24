using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BestowPowerup : MonoBehaviour {

    public List<string> tagFilters;
    public string nameOfClass;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll) {
        var go = coll.gameObject;
        if( tagFilters.Contains(go.tag)) {
            Debug.Log("ALLOWING " + go.tag);
            var pm = go.GetComponent<PowerupManager>();
            if( pm != null) {
                Debug.Log("POWERUP MANAGER NOT EMPTY");
                var g = (IPowerup)System.Activator.CreateInstance(
                    System.Reflection.Assembly.GetExecutingAssembly().FullName,
                    nameOfClass
                ).Unwrap();
                g.Init();
                pm.CollectPowerup(g);
                Destroy(gameObject);
            }
        }
    }
}
