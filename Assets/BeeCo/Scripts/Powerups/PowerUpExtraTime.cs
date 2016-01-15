using UnityEngine;
using System.Collections;

public class PowerUpExtraTime : PowerUpItem {
    public float timeToGive = 30.0f;

    public override void Attach(GameObject attachTo) {
        paddle = attachTo.GetComponent<Paddle>();

        if (paddle) {
            God.haggleLogic.time += timeToGive;
            var t = God.SpawnAt(God.main.hitText, transform.position);
            var fta = t.GetComponent<FloatTextAway>();
            fta.SetText("+" + God.FormatTime(timeToGive), true);
            Destroy(gameObject);
        } else {
            base.Attach(attachTo);
        }
    }
}