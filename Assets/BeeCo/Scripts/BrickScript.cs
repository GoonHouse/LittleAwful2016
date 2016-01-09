using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrickScript : MonoBehaviour {
    public int Health = 3;
    public List<Material> materials;
    float countDown = .05f;


    public void TakeDamage(){
        Health--;
        var rend = GetComponent<Renderer>();
        rend.sharedMaterial = materials[Health-1];
    }

    void Update(){
        if (Health<=0){
            countDown -= Time.deltaTime;

            if (countDown <= 0){
                Destroy(gameObject);
            }
        }
    }
}
