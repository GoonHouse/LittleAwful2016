﻿using UnityEngine;
using System.Collections;

public abstract class BaseFamiliar : MonoBehaviour, IFamiliar {

    public float baseEnergy = 100.0f;
    public float baseEnergyPerUse = 20.0f;
    public float baseEnergyGainedPerSecond = 2.00f;
    public bool requiresTarget = true;

    private float energy;
    private float energyGainedPerSecond;

    // Use this for initialization
    virtual public void Awake () {
        energy = baseEnergy;
        energyGainedPerSecond = baseEnergyGainedPerSecond;
	}
	
	// Update is called once per frame
	virtual public void Update () {
        var canUse = energy >= baseEnergyPerUse;
        energy += energyGainedPerSecond * Time.deltaTime;
        if( !canUse && ( energy >= baseEnergyPerUse )) {
            Instantiate(
                Resources.Load("Effects/BurstEffect") as GameObject,
                transform.position,
                transform.rotation
            );
        }
	}

    virtual public float GetEnergyLeft() {
        return (energy / baseEnergy);
    }

    virtual public bool ConsumeShot(AbstractPlayer player, Vector3 pos) {
        if( energy >= baseEnergyPerUse) {
            if( requiresTarget && player.focusedThing == null) {
                return false;
            } else {
                var ret = DoAbility(player, pos);
                if( ret ) {
                    energy -= baseEnergyPerUse;
                }
                return ret;
            }
        } else {
            return false;
        }
    }

    virtual public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        var ball = player.focusedThing;
        var scale = ball.transform.localScale;
        scale *= 1.25f;
        ball.transform.localScale = scale;
        return true;
    }
}