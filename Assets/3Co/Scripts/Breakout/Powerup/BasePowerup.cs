using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasePowerup : IPowerup {
    virtual public void Update(float dt) {
        Debug.Log("oh boy i love horses: " + dt);
    }

    virtual public void OnTriggerEnter(Collider other) {
    }

    virtual public void OnCollect(GameObject go) {
        Debug.Log("got connected to: " + go.name);
    }

    virtual public void ToPatrolState() {
    }

    virtual public void ToAlertState() {
    }

    virtual public void ToChaseState() {
    }
}
