using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickScript : MonoBehaviour {
    public int startHealth = 3;
    public int health = 3;
    public List<Material> materials;
    public float damageDelay = .05f;

    public int TakeDamage(int amount = 1){
        health -= amount;
        
        if( health > 0) {
            var rend = GetComponent<Renderer>();
            rend.sharedMaterial = materials[health - 1];
        }

        return health;
    }

    void Update(){
        if( health <= 0 ){
            damageDelay -= Time.deltaTime;

            if( damageDelay <= 0 ){
                Destroy(gameObject);
            }
        }
    }
}
