using UnityEngine;
using System.Collections;

public abstract class BaseFamiliar : MonoBehaviour, IFamiliar {

    public float baseEnergy = 100.0f;
    public float baseEnergyPerUse = 20.0f;
    public bool requiresTarget = true;

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
        if( energy >= baseEnergyPerUse) {
            if( requiresTarget && ball == null) {
                return false;
            } else {
                energy -= baseEnergyPerUse;
                return DoAbility(player, ball);
            }
        } else {
            return false;
        }
    }

    virtual public bool DoAbility(PlayerPaddle player, BaseBall ball) {
        var scale = ball.gameObject.transform.localScale;
        scale *= 2;
        ball.gameObject.transform.localScale = scale;
        return true;
    }
}
