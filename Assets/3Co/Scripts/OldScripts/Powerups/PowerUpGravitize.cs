using UnityEngine;
using System.Collections;

public class PowerUpGravitize : PowerUpItem {
    public override float BallEffect() {
        foreach (GameObject ball in paddle.spawnedBalls) {
            ball.GetComponent<HaggleBall>().Gravitize();
        }
        return 1.0f;
    }
}
