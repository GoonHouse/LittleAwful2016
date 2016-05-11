using UnityEngine;
using System.Collections;

public class BallsFamiliar : BaseFamiliar {

    override public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        var npos = transform.position;
        npos.x += 1.0f;
        if( !player.CanSpawnSomethingHere(pos) ) {
            return false;
        }
        var tball = (GameObject)Instantiate(player.ball, npos, Quaternion.identity);
        tball.transform.SetParent(GameObject.Find("BallHell").transform, true);
        tball.GetComponent<PowerupManager>().owner = gameObject;
        tball.GetComponent<BaseBall>().owner = gameObject;
        tball.GetComponent<BaseBall>().creator = player;
        return true;
    }

}
