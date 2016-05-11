using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPaddle : AbstractPlayer {
    // Combo Stuff
    public int baseNumBallsCanSpawn = 1;

    // Ball Handling
    //public int numBallsCanSpawn;
    //public List<GameObject> spawnedBalls;

    // WHERE BALLS?
    // BALLS WHERE
    // HELP

    virtual public bool NotUI() {
        return !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    override public bool PrimaryButton() {
        return Input.GetMouseButtonDown(0);
    }

    override public bool SecondaryButton() {
        return Input.GetMouseButtonDown(1);
    }

    override public bool SecondaryButtonRelease() {
        return Input.GetMouseButtonUp(1);
    }

    override public void GoWhereIShould() {
        whereIShouldBe.y = God.Scale(Input.mousePosition.y, 0, Screen.height, -howFarToGo, howFarToGo);
        base.GoWhereIShould();
    }

    // Update is called once per frame
    void Update() {
        base.Update();

        /*
        if(GetActiveFamiliar() != null && GetActiveFamiliar().GetEnergyLeft() > 0.0f) {
            familiars[activeFamiliarIndex].GetComponent<Circle>().enabled = true;
        }
        */
        /*
        if (SecondaryButton() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            FocusBallNearest(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        */

        if (PrimaryButton() && NotUI()) {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UseActiveFamiliar(pos);
        }

        if (SecondaryButton() && NotUI()) {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            focusedThing = GetNearest<BaseBall>(pos).gameObject;
        }

        if ( focusedThing != null ) {
            focusVisualiser.transform.position = focusedThing.transform.position;
            focusVisualiser.transform.Rotate(0.0f, 0.0f, 0.3f);

            if( SecondaryButtonRelease() && NotUI()) {
                focusedThing = null;
            }
        }

        var mouse = Input.GetAxis("Mouse ScrollWheel");
        if (mouse < 0.0f) {
            TargetNudge(1);
        } else if( mouse > 0.0f) {
            TargetNudge(-1);
        }

        /*
        if(Input.GetMouseButtonDown(2)) {
            FocusBallNearest(transform.position);
        }
        */
    }
}
