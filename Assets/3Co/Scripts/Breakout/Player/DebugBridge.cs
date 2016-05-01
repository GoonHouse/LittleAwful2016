using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DebugBridge : MonoBehaviour {
    public GameObject player;
    public PlayerPaddle paddle;
    public GameObject enemy;
    public Brickster brickster;
    public Text ballNo;
    public Slider energyVisualiser;
    public List<Button> famButtons;
    public Button doOnButton;
    public Slider playerHealth;
    public Slider enemyHealth;

    // Use this for initialization
    void Start() {
        paddle = player.GetComponent<PlayerPaddle>();
    }

    // Update is called once per frame
    void Update() {
        var af = paddle.GetActiveFamiliar();
        energyVisualiser.value = af.GetEnergyLeft();

        var i = 0;
        foreach(Button button in famButtons) {
            var bf = paddle.familiars[i];
            if( bf.GetEnergyLeft() <= 0.0f) {
                button.interactable = false;
            }
            i++;
        }

        playerHealth.value = player.GetComponent<Health>().GetHealth();
        enemyHealth.value = enemy.GetComponent<Health>().GetHealth();
    }

    public void SetFamiliar(int index) {
        paddle.activeFamiliarIndex = index;
    }
}
