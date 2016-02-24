using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasePowerup : IPowerup {

    // = PARENT
    public GameObject owner;

    // = EFFECTS
    public string effectToChild;
    public GameObject childedEffect;

    virtual public void Init() {
    }

    virtual public void Update(float dt) {
        Debug.Log("oh boy i love horses: " + dt);
    }

    virtual public void OnTriggerEnter(Collider other) {
    }

    // Methods
    virtual public void ToggleEffect(bool state) {
        if (effectToChild != null && state ) {
            childedEffect = GameObject.Instantiate(
                Resources.Load(effectToChild) as GameObject,
                owner.transform.position,
                owner.transform.rotation
            ) as GameObject;
            childedEffect.transform.SetParent(owner.transform, true);
        } else if( childedEffect != null && !state) {
            GameObject.Destroy(childedEffect);
        } else {
            Debug.LogWarning("WHAT HAPPENED");
        }
    }

    // ON signals
    virtual public void OnCollect(GameObject go) {
        Debug.Log("GOT COLLECTED BY " + go.name);
        owner = go;
        ToggleEffect(true);
    }
}
