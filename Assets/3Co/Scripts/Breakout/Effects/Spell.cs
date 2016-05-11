using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IOnCast {
    void Action(Transform origin, Transform destination);
}

public interface IOnArrive {
    void Action(Transform origin, Transform destination);
}

public class TestArriveEffect : IOnArrive {
    public void Action(Transform origin, Transform destination) {
        var go = (GameObject)GameObject.Instantiate(
            Resources.Load("Effects/BurstEffect") as GameObject,
            destination.position,
            destination.rotation
        );
        go.transform.SetParent(destination);
    }
}

public class Spell : MonoBehaviour {
    public List<IOnCast> OnCastEffects = new List<IOnCast>();
    public List<IOnArrive> OnArriveEffects = new List<IOnArrive>();
    public Transform origin;
    public Transform destination;
    public float travelTime;

    private float timeTraveled;
    private bool wasCast = false;

    public void Cast(Transform origin, Transform destination, float travelTime) {
        this.origin = origin;
        this.destination = destination;
        this.travelTime = travelTime;

        wasCast = true;

        foreach (IOnCast oc in OnCastEffects) {
            oc.Action(origin, destination);
        }
    }

    public void Update() {
        if( wasCast ) {
            timeTraveled += Time.deltaTime;

            transform.position = Vector3.Lerp(origin.position, destination.position, timeTraveled / travelTime);

            if( timeTraveled >= travelTime) {
                foreach (IOnArrive oa in OnArriveEffects) {
                    oa.Action(origin, destination);
                }

                Destroy(gameObject);
            }
        }
    }
}