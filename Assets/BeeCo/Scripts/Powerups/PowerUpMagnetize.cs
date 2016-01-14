using UnityEngine;
using System.Collections;

public class PowerUpMagnetize : PowerUpItem {
    public bool toggleDrain = false;

    public override void UnDoAction() {
        toggleDrain = false;
        foreach (GameObject ball in paddle.spawnedBalls) {
            ball.GetComponent<HaggleBall>().Demagnetize();
        }
        ResolveSuicide();
    }

    public override float BallEffect() {
        toggleDrain = true;

        foreach (GameObject ball in paddle.spawnedBalls) {
            ball.GetComponent<HaggleBall>().Magnetize(paddle.gameObject, true);
        }
        return 0.0f;
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        if( toggleDrain ) {
            uses -= usePerAction * Time.fixedDeltaTime;
            ResolveSuicide();
        }
    }
}
