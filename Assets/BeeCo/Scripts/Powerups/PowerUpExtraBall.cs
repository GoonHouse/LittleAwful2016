using UnityEngine;
using System.Collections;

public class PowerUpExtraBall : PowerUpItem {
    public override float BallEffect() {
        paddle.SpawnBall();
        return 1.0f;
    }
}
