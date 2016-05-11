using UnityEngine;
using System.Collections;

public class BallsFamiliar : BaseFamiliar {

    override public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        if( !player.CanSpawnSomethingHere(pos) ) {
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
