using UnityEngine;
using System.Collections;

[System.Serializable]
public class DebugScriptedPowerup : ScriptedPowerup {

    override public void Awake() {
        effectToChild = "Effects/DebugTrail";
        base.Start();
    }
}
