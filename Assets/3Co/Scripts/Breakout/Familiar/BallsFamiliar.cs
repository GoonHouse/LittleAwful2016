using UnityEngine;
using System.Collections;

public class BallsFamiliar : BaseFamiliar {

    override public bool DoAbility(PlayerPaddle player, BaseBall ball) {
        return player.SpawnBall();
    }

}
