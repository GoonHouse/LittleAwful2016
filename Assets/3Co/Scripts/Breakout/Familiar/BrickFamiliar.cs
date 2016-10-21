using UnityEngine;
using System.Collections;

public class SpawnABrick : IOnArrive {
    public void Action(Spell spell) {
        Quaternion rot = Quaternion.identity;
        Vector3 pos = spell.transform.position;
        if (spell.destinationTransform) {
            rot = spell.destinationTransform.rotation;
            pos = spell.destinationTransform.position;
        }
        if (spell.player.CanSpawnSomethingHere(pos)) {
            var bbrick = (GameObject)GameObject.Instantiate(spell.player.brick, pos, Quaternion.identity);
            bbrick.transform.SetParent(GameObject.Find("BrickHell").transform, true);
            var go = (GameObject)GameObject.Instantiate(
                Resources.Load("Effects/BurstEffect") as GameObject,
                pos,
                rot
            );
            return;
        } else {
            Debug.Log("boners?");
        }
    }
}

public class BrickFamiliar : BaseFamiliar {
    override public bool QueueAbilities(Spell spell) {
        base.QueueAbilities(spell);
        spell.OnArriveEffects.Add(new SpawnABrick());
        return true;
    }
}
