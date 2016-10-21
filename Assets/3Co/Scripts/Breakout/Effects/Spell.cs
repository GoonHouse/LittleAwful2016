using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IOnCast {
    void Action(Spell spell);
}

public interface IOnArrive {
    void Action(Spell spell);
}

public class TestArriveEffect : IOnArrive {
    public void Action(Spell spell) {
        Quaternion rot = Quaternion.identity;
        Vector3 pos = spell.transform.position;
        if( spell.destinationTransform ){
            rot = spell.destinationTransform.rotation;
            pos = spell.destinationTransform.position;
        }
        var go = (GameObject)GameObject.Instantiate(
            Resources.Load("Effects/BurstEffect") as GameObject,
            pos,
            rot
        );
        //go.transform.SetParent(destination);
    }
}

public class Spell : MonoBehaviour {
    public List<IOnCast> OnCastEffects = new List<IOnCast>();
    public List<IOnArrive> OnArriveEffects = new List<IOnArrive>();

    public Transform destinationTransform;
    public Vector3 destination;
    public float travelTime;

    public AbstractPlayer player;
    public BaseFamiliar fam;
    public GameObject focusedThing;

    private float timeTraveled;
    private bool wasCast = false;

    public void Cast(AbstractPlayer player, BaseFamiliar fam, float travelTime) {
        this.player = player;
        this.fam = fam;
        this.travelTime = travelTime;
        this.focusedThing = player.focusedThing;

        if( player.focusedThing != null) {
            destinationTransform = player.focusedThing.transform;
        }
        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = 0.0f;

        wasCast = true;

        foreach (IOnCast oc in OnCastEffects) {
            oc.Action(this);
        }
    }

    public void Update() {
        if( wasCast ) {
            timeTraveled += Time.deltaTime;

            Vector3 dest;
            if( destinationTransform != null) {
                dest = destinationTransform.transform.position;
            } else {
                dest = destination;
            }
            transform.position = Vector3.Lerp(fam.transform.position, dest, timeTraveled / travelTime);

            if( timeTraveled >= travelTime) {
                foreach (IOnArrive oa in OnArriveEffects) {
                    oa.Action(this);
                }

                Destroy(gameObject);
            }
        }
    }
}