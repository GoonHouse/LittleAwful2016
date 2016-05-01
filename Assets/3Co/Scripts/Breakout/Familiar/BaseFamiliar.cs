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

    virtual public bool ConsumeShot(AbstractPlayer player, BaseBall ball, Vector3 pos) {
        if( energy >= baseEnergyPerUse) {
            if( requiresTarget && ball == null) {
                return false;
            } else {
                var ret = DoAbility(player, ball, pos);
                if( ret ) {
                    energy -= baseEnergyPerUse;
                }
                return ret;
            }
        } else {
            return false;
        }
    }

    virtual public bool DoAbility(AbstractPlayer player, BaseBall ball, Vector3 pos) {
        var scale = ball.gameObject.transform.localScale;
        scale *= 2;
        ball.gameObject.transform.localScale = scale;
        return true;
    }
}
