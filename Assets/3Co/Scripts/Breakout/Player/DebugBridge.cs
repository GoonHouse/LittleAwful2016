using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DebugBridge : MonoBehaviour {
    public GameObject player;
    public GameObject enemy;

    public Slider playerHealth;
    public Slider enemyHealth;

    // Update is called once per frame
    void Update() {
        playerHealth.value = player.GetComponent<Health>().GetHealth();
        enemyHealth.value = enemy.GetComponent<Health>().GetHealth();
    }
}
