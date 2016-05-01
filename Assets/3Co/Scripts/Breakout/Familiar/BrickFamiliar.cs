using UnityEngine;
using System.Collections;

public class BrickFamiliar : BaseFamiliar {

    public GameObject brick;

    override public bool DoAbility(PlayerPaddle player, BaseBall ball) {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        GameObject.Instantiate(brick, pos, Quaternion.identity);
        return true;
    }

}
