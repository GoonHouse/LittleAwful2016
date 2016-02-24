using UnityEngine;
using System.Collections;

[System.Serializable]
public class DebugPowerup : BasePowerup {

    public string effectToChild = "Effects/DebugTrail";
    public GameObject childedEffect;

    override public void Update(float dt) {
        //Debug.Log("oh boy i love horses: " + dt);
    }

    override public void OnTriggerEnter(Collider other) {
    }

    override public void OnCollect(GameObject go) {
        Debug.Log("got connected to: " + go.name);
        childedEffect = GameObject.Instantiate(
            Resources.Load(effectToChild) as GameObject,
            go.transform.position,
            go.transform.rotation
        ) as GameObject;
        childedEffect.transform.SetParent(go.transform, true);
        Debug.Log(childedEffect);
        Debug.Log(childedEffect.name);
    }

    override public void ToPatrolState() {
    }

    override public void ToAlertState() {
    }

    override public void ToChaseState() {
    }
}
