using UnityEngine;
using System.Collections;

public interface IPowerup {

    void Init();

    void Update(float dt);

    void OnTriggerEnter(Collider other);

    void OnCollect(GameObject go);
}