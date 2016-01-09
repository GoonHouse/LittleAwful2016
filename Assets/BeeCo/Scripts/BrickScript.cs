using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickScript : MonoBehaviour {
    public int health = 3;
    public List<Material> materials;
    float damageDelay = .05f;

    public void TakeDamage(){
        health--;
        
        if( health > 0) {
            var rend = GetComponent<Renderer>();
            rend.sharedMaterial = materials[health - 1];
        }
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
