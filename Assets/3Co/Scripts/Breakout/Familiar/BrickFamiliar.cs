using UnityEngine;
using System.Collections;

public class BrickFamiliar : BaseFamiliar {

    public GameObject brick;

    override public bool DoAbility(AbstractPlayer player, BaseBall ball, Vector3 pos) {
        Debug.Log("WATASHI WA UNTRACEABLE ERROR DES");
        pos.z = 0;
        if( player.CanSpawnSomethingHere( pos ) ) {
            Debug.Log("EVERYTHING LOOKS FINE TO ME, PERHAPS YOU SHOULD CALL THE POLICE?");
            GameObject.Instantiate(brick, pos, Quaternion.identity);
            return true;
        }
        return false;
    }
}
