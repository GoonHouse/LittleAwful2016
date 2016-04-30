using UnityEngine;
using System.Collections;

public abstract class BaseFamiliar : MonoBehaviour, IFamiliar {

    public float baseEnergy = 100.0f;
    public float baseEnergyPerUse = 20.0f;

    private float energy;

    // Use this for initialization
    virtual public void Awake () {
        energy = baseEnergy;
	}
	
	// Update is called once per frame
	virtual public void Update () {
	
	}

    virtual public float GetEnergyLeft() {
        return (energy / baseEnergy);
    }

    virtual public bool ConsumeShot(PlayerPaddle player, BaseBall ball) {
        Debug.Log("ABOUT TO FUCKIN BLOW");
        if( energy >= baseEnergyPerUse) {
            Debug.Log("SHIT EAH BOYIE");
            energy -= baseEnergyPerUse;
            var scale = ball.gameObject.transform.localScale;
            scale *= 2;
            ball.gameObject.transform.localScale = scale;
            return true;
        } else {
            Debug.Log("fug :D))");
            return false;
        }
    }
}
