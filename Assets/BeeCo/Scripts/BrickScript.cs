using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickScript : MonoBehaviour {
    public float startHealth = 3;
    public float health = 3;
    public List<Material> materials;
    public float damageDelay = 0.05f;

    private bool gonnaDie = false;

    public GameObject powerupToSpawn;

    public float TakeDamage(float amount = 1.0f){
        health -= amount;
        
        if( health > 0.0f ) {
            var rend = GetComponent<Renderer>();
            rend.sharedMaterial = materials[Mathf.RoundToInt(health) - 1];
        }

        return health;
    }

    void Update(){
        if( health <= 0.0f && !gonnaDie ){
            gonnaDie = true;
            Debug.LogWarning(gameObject.name + " is about to die!");
            StartCoroutine("Die");
        }
    }

    IEnumerator Die() {
        Debug.LogWarning(gameObject.name + " is yielding to death!");
        yield return new WaitForSeconds(damageDelay);
        Debug.LogWarning(gameObject.name + " waited for death!");

        if ( powerupToSpawn != null) {
            Debug.LogWarning(gameObject.name + " is gonna spawn a powerup!");
            var powerUp = (GameObject)Instantiate(powerupToSpawn, transform.position, Quaternion.identity);
            powerUp.GetComponent<PowerUpItem>().Born();
        }

        Debug.LogWarning(gameObject.name + " is killing itself!");
        Destroy(gameObject);
    }
}
