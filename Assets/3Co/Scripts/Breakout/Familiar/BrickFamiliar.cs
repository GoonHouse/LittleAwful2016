using UnityEngine;
using System.Collections;

public class SpawnABrick : IOnArrive {
    public void Action(Transform origin, Transform destination) {
        var 

        pos.z = 0;
        if (player.CanSpawnSomethingHere(pos)) {
            var bbrick = (GameObject)GameObject.Instantiate(brick, pos, Quaternion.identity);
            bbrick.transform.SetParent(GameObject.Find("BrickHell").transform, true);
            return true;
        }
        return false;

        var go = (GameObject)GameObject.Instantiate(
            Resources.Load("Effects/BurstEffect") as GameObject,
            destination.position,
            destination.rotation
        );
        //go.transform.SetParent(destination);
    }
}

public class BrickFamiliar : BaseFamiliar {

    public GameObject brick;

    override public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        
    }
}
