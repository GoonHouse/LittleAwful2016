using UnityEngine;
using System.Collections;

[System.Serializable]
public class DebugPowerup : BasePowerup {
    public override void PowerupUpdate(float dt) {
        Debug.Log("oh boy i love horses: " + dt);
    }

    public override void PowerupOnTriggerEnter(Collider other) {
    }

    public override void PowerupToPatrolState() {
    }

    public override void PowerupToAlertState() {
    }

    public override void PowerupToChaseState() {
    }
}
