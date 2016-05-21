using UnityEngine;
using System.Collections;

/*
public class BallSpawner : IOnArrive {
    public void Action(Transform origin, Transform destination) {
        if (!player.CanSpawnSomethingHere(destination.position)) {
            return false;
        }
        var tball = (GameObject)Instantiate(player.ball, transform.position, Quaternion.identity);
        tball.transform.SetParent(GameObject.Find("BallHell").transform, false);
        tball.GetComponent<PowerupManager>().owner = gameObject;
        tball.GetComponent<BaseBall>().owner = gameObject;
        tball.GetComponent<BaseBall>().creator = player;
        return true;
    }
}

public class TestArriveEffect : IOnArrive {
    public void Action(Transform origin, Transform destination) {
        var go = (GameObject)GameObject.Instantiate(
            Resources.Load("Effects/BurstEffect") as GameObject,
            destination.position,
            destination.rotation
        );
        //go.transform.SetParent(destination);
    }
}
*/

public class BallsFamiliar : BaseFamiliar {

    override public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        return false;
    }

}
