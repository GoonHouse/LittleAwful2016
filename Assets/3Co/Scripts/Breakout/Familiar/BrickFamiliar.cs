using UnityEngine;
using System.Collections;

public class BrickFamiliar : BaseFamiliar {

    public GameObject brick;

    override public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        pos.z = 0;
        if( player.CanSpawnSomethingHere( pos ) ) {
            var bbrick = (GameObject)GameObject.Instantiate(brick, pos, Quaternion.identity);
            bbrick.transform.SetParent(GameObject.Find("BrickHell").transform, true);
            return true;
        }
        return false;
    }
}
