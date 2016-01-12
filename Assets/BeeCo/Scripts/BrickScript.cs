using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickScript : MonoBehaviour {
    public float startHealth = 3;
    public float health = 3;
    public List<Material> materials;
    public float damageDelay = 0.05f;

    public float TakeDamage(float amount = 1.0f){
        health -= amount;
        
        if( health > 0.0f ) {
            var rend = GetComponent<Renderer>();
            rend.sharedMaterial = materials[Mathf.RoundToInt(health) - 1];
        }

        return health;
    }

    void Update(){
        if( health <= 0.0f ){
            damageDelay -= Time.deltaTime;

            if( damageDelay <= 0 ){
                Destroy(gameObject);
            }
        }
    }
}
