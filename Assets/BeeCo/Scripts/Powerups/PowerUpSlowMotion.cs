using UnityEngine;
using System.Collections;

public class PowerUpSlowMotion : PowerUpItem {
    public bool toggleDrain = false;
    public bool magneticDirection = true;
    public override void UnDoAction() {
        toggleDrain = false;
        foreach (GameObject ball in paddle.spawnedBalls) {
            ball.GetComponent<HaggleBall>().NormalMotion();
        }
    }

    public override float BallEffect() {
        toggleDrain = true;

        foreach (GameObject ball in paddle.spawnedBalls) {
            ball.GetComponent<HaggleBall>().SlowMotion();
        }
        return 0.0f;
    }

    public override void FixedUpdate() {
        base.FixedUpdate();
        if( toggleDrain ) {
            uses -= usePerAction * Time.fixedDeltaTime;
        }
        ResolveSuicide();
    }
}
