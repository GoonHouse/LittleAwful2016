using UnityEngine;
using System.Collections;

[System.Serializable]
public class DebugPowerup : BasePowerup {

    override public void Init() {
        effectToChild = "Effects/DebugTrail";
    }
}
