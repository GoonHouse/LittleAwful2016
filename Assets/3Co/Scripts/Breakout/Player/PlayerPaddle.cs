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

    override public bool PrimaryButton() {
        return Input.GetMouseButtonDown(0);
    }

    override public bool SecondaryButton() {
        return Input.GetMouseButtonDown(1);
    }

    override public void GoWhereIShould() {
        whereIShouldBe.y = God.Scale(Input.mousePosition.y, 0, Screen.height, -howFarToGo, howFarToGo);
        base.GoWhereIShould();
    }

    // Update is called once per frame
    void Update() {
        GoWhereIShould();

        foreach(BaseFamiliar fam in familiars) {
            var line = fam.gameObject.GetComponent<LineRenderer>();
            line.enabled = false;
            if (GetActiveFamiliar().GetComponent<BaseFamiliar>() == fam && fam.GetEnergyLeft() > 0.0f) {
                var circle = fam.gameObject.GetComponent<Circle>();
                line.enabled = true;
                circle.segments = (int)fam.baseEnergy;
                circle.visualParts = fam.GetEnergyLeft();
            }
        }

        /*
        if(GetActiveFamiliar() != null && GetActiveFamiliar().GetEnergyLeft() > 0.0f) {
            familiars[activeFamiliarIndex].GetComponent<Circle>().enabled = true;
        }
        */

        if (SecondaryButton() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            FocusBallNearest(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (PrimaryButton() && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UseActiveFamiliar(pos);
        }

        if ( focusedThing != null ) {
            focusVisualiser.transform.position = focusedThing.transform.position;
        }

        var mouse = Input.GetAxis("Mouse ScrollWheel");
        if (mouse < 0.0f) {
            TargetNudge(1);
        } else if( mouse > 0.0f) {
            TargetNudge(-1);
        }

        if(Input.GetMouseButtonDown(2)) {
            FocusBallNearest(transform.position);
        }
    }
}
