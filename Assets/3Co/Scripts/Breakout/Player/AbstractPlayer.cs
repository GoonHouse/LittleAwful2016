using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractPlayer : MonoBehaviour {
    public GameObject ball;
    public GameObject brick;
    public GameObject brickZone;

    public GameObject focusedThing;
    public GameObject focusVisualiser;

    public List<BaseFamiliar> familiars;

    public int activeFamiliarIndex = 0;

    public Vector3 whereIShouldBe;

    // how far the paddle can move relative from its top / bottom
    public float howFarToGo = 3.50f;

    virtual public void Start() {
        whereIShouldBe.x = transform.position.x;
    }

    public void Reset() {
        GetComponent<Combos>().CancelCombo();
    }

    virtual public void GoWhereIShould() {
        var rigid = GetComponent<Rigidbody2D>();
        rigid.MovePosition(whereIShouldBe);
    }

    virtual public bool CanSpawnSomethingHere(Vector3 pos) {
        var coll = brickZone.GetComponent<BoxCollider2D>().bounds;

        // great fuckin' job you messed up every single comparison out of 4 ~ej, to ej
        if( coll.max.x >= pos.x && pos.x >= coll.min.x &&
            coll.max.y >= pos.y && pos.y >= coll.min.y) {
                return true;
        }
        return false;
    }

    virtual public void FindAPlaceToSpawn(GameObject thing) {
        var coll = brickZone.GetComponent<BoxCollider2D>().bounds;

        Vector3 pos;
        Vector3 min = coll.min;
        Vector3 max = coll.max;

        var bbounds = brick.GetComponent<BoxCollider2D>().bounds;

        min -= bbounds.extents;
        max -= bbounds.extents;

        pos.x = Random.Range(min.x, max.x);
        pos.y = Random.Range(min.y, max.y);
        pos.z = Random.Range(min.z, max.z);
    }

    virtual public bool PrimaryButton() {
        Debug.Log("poot");
        return false;
    }

    virtual public bool SecondaryButton() {
        Debug.Log("poot");
        return false;
    }

    virtual public bool UseActiveFamiliar(Vector3 pos) {
        var af = GetActiveFamiliar();
        var fb = GetFocusedBall();
        return af.ConsumeShot(this, fb, pos);
    }

    public BaseFamiliar GetActiveFamiliar() {
        return familiars[activeFamiliarIndex];
    }

    public BaseBall GetFocusedBall() {
        if (focusedThing != null) {
            return focusedThing.GetComponent<BaseBall>();
        }
        return null;
    }

    public void FocusBallNearest(Vector3 position) {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (BaseBall g in GameObject.FindObjectsOfType<BaseBall>()) {
            float dist = Vector3.Distance(g.transform.position, position);
            if (dist < minDist) {
                tMin = g.transform;
                minDist = dist;
            }
        }
        focusedThing = tMin.gameObject;
    }

    public void TargetNudge(int direction) {
        var numFamiliars = familiars.Count;
        var newPos = (activeFamiliarIndex + direction);
        activeFamiliarIndex = Mod(newPos, numFamiliars);
        //act = paddle.spawnedBalls[paddle.focusedBallIndex];
    }

    public int Mod(int a, int b) {
        return (a % b + b) % b;
    }
}
