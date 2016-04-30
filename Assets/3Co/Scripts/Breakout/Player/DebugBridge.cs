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
        ballNo.text = System.String.Format("{0}/{1}", paddle.focusedBallIndex, paddle.spawnedBalls.Count-1);
        var af = paddle.GetActiveFamiliar();
        energyVisualiser.value = af.GetEnergyLeft();

        if(doOnButton.interactable == false && af.GetEnergyLeft() > 0.0f) {
            doOnButton.interactable = true;
        }

        var i = 0;
        foreach(Button button in famButtons) {
            var bf = paddle.familiars[i];
            if( bf.GetEnergyLeft() <= 0.0f) {
                button.interactable = false;
                if (bf == paddle.GetActiveFamiliar()) {
                    doOnButton.interactable = false;
                }
            }
            i++;
        }

        playerHealth.value = player.GetComponent<Health>().GetHealth();
        enemyHealth.value = enemy.GetComponent<Health>().GetHealth();
    }

    int Mod(int a, int b) {
        return (a % b + b) % b;
    }

    public void TargetNudge(int direction) {
        var numBalls = paddle.spawnedBalls.Count;
        var newPos = (paddle.focusedBallIndex + direction);
        paddle.focusedBallIndex = Mod(newPos, numBalls);
        paddle.focusedBall = paddle.spawnedBalls[paddle.focusedBallIndex];
    }

    public void SetFamiliar(int index) {
        paddle.activeFamiliarIndex = index;
    }

    public void Consume() {
        var af = paddle.GetActiveFamiliar();
        var fb = paddle.GetFocusedBall();
        Debug.Log(af.ConsumeShot(paddle, fb));
    }
}
