using UnityEngine;
using System.Collections;

[System.Serializable]
public class BasePowerup : IPowerup {

    // = PARENT
    public GameObject owner;

    // = EFFECTS
    public string effectToChild;
    public GameObject childedEffect;

    // = AMMO/CONSUMPTION/BATTERY
    public bool continuousUse = false;
    public float maxAmmo = 3.0f;
    public float ammoPerUse = 1.0f;
    public float numAmmo;

    virtual public void Init() {
        numAmmo = maxAmmo;
    }

    virtual public void Update(float dt) {
        if( Input.GetMouseButtonDown(0) && CanUse() ) {
            ConsumeAmmo();
            DoUse();
        }
    }

    virtual public void FixedUpdate(float dt) {
        //Debug.Log("oh boy i love fixed horses: " + dt);
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

    virtual public bool CanUse() {
        return (numAmmo >= ammoPerUse);
    }

    virtual public void ConsumeAmmo() {
        numAmmo -= ammoPerUse;
    }

    virtual public void DoUse() {
        Debug.Log("oh boy i love horses: " + Time.deltaTime);
    }

    // ON signals
    virtual public void OnCollect(GameObject go) {
        Debug.Log("GOT COLLECTED BY " + go.name);
        owner = go;
        ToggleEffect(true);
    }
}
