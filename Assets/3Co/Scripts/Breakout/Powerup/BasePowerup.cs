using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasePowerup {

    public virtual void PowerupUpdate(float dt) { }

    public virtual void PowerupOnTriggerEnter(Collider other) { }

    public virtual void PowerupToPatrolState() { }

    public virtual void PowerupToAlertState() { }

    public virtual void PowerupToChaseState() { }
}