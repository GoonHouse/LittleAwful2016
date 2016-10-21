using UnityEngine;
using System.Collections;

public abstract class BaseFamiliar : MonoBehaviour, IFamiliar {

    public float baseEnergy = 100.0f;
    public float baseEnergyPerUse = 20.0f;
    public float baseEnergyGainedPerSecond = 2.00f;
    public bool requiresTarget = true;

    public StatusIndicator status;
    public AbstractPlayer player;

    private float energy;
    private float energyGainedPerSecond;

    // Use this for initialization
    virtual public void Awake () {
        energy = baseEnergy;
        energyGainedPerSecond = baseEnergyGainedPerSecond;
        //status = GetComponentInChildren<StatusIndicator>();
	}
	
	// Update is called once per frame
	virtual public void Update () {
        if( energy <= baseEnergy) {
            var canUse = energy >= baseEnergyPerUse;
            energy += energyGainedPerSecond * Time.deltaTime;
            if (!canUse && (energy >= baseEnergyPerUse)) {
                Instantiate(
                    Resources.Load("Effects/BurstEffect") as GameObject,
                    transform.position,
                    transform.rotation
                );
            }
        }
	}

    virtual public float GetEnergyLeft() {
        return (energy / baseEnergy);
    }

    virtual public bool CanShoot() {
        return (energy >= baseEnergyPerUse);
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

    virtual public bool QueueAbilities(Spell spell) {
        spell.OnArriveEffects.Add(new TestArriveEffect());
        return true;
    }

    virtual public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        var ce = (GameObject)Instantiate(
            Resources.Load("Effects/CastEffect") as GameObject,
            player.transform.position,
            player.transform.rotation
        );
        var spell = ce.GetComponent<Spell>();
        spell.destination = pos;

        if (!QueueAbilities(spell)) {
            return false;
        };

        spell.Cast(player, this, 1.0f);

        return true;
    }
}
