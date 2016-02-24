using UnityEngine;
using System.Collections;

public interface IPowerup {
    void Update(float dt);

    void OnTriggerEnter(Collider other);

    void OnCollect(GameObject go);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();
}