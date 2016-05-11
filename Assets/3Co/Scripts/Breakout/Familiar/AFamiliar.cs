using UnityEngine;
using System.Collections;

public class BallEmbiggener : IOnArrive {
    public void Action(Transform origin, Transform destination) {
        var scale = destination.localScale;
        scale *= 1.25f;
        destination.localScale = scale;
    }
}

public class AFamiliar : BaseFamiliar {
    // don't overwrite nothing because we do jack and also shit
    //public class Base
    override public bool DoAbility(AbstractPlayer player, Vector3 pos) {
        var ce = (GameObject)Instantiate(
            Resources.Load("Effects/CastEffect") as GameObject,
            player.transform.position,
            player.transform.rotation
        );
        var spell = ce.GetComponent<Spell>();

        spell.OnArriveEffects.Add(new TestArriveEffect());
        spell.OnArriveEffects.Add(new BallEmbiggener());

        spell.Cast(player.transform, player.focusedThing.transform, 1.0f);
        
        //var ball = player.focusedThing;
        //var scale = ball.transform.localScale;
        //scale *= 1.25f;
        //ball.transform.localScale = scale;
        return true;
    }
}
