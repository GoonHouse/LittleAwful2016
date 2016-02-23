using UnityEngine;
using System.Collections;

[System.Serializable]
public class DebugPowerup : IPowerup {
    public void Update(float dt) {
        Debug.Log("oh boy i love horses: " + dt);
    }

    public void OnTriggerEnter(Collider other) {
    }

    public void ToPatrolState() {
    }

    public void ToAlertState() {
    }

    public void ToChaseState() {
    }
}
