using UnityEngine;
using System.Collections;

/*
public class KeepGoingFaster : IGetMaxSpeed, IGetMinSpeed {
    public float Action(Rigidbody2D rigid, float nowSpeed, float baseSpeed) {
        return nowSpeed * 1.005f;
    }
}

public class GoReallyFastAllTheTime : IGetMaxSpeed, IGetMinSpeed {
    public float Action(Rigidbody2D rigid, float nowSpeed, float baseSpeed) {
        return 80.0f;
    }
}

public class MakeABallGoFast : IOnArrive {
    public void Action(Transform origin, Transform destination) {
        var bb = destination.gameObject.GetComponent<BaseBall>();
        bb.GetMinSpeed.Add(new KeepGoingFaster());
        bb.GetMaxSpeed.Add(new GoReallyFastAllTheTime());
    }
}
*/

public class BallEmbiggener : IOnArrive {
    public void Action(Spell spell) {
        var scale = spell.player.focusedThing.transform.localScale;
        scale *= 1.25f;
        spell.player.focusedThing.transform.localScale = scale;
    }
}

public class AFamiliar : BaseFamiliar {
    override public bool QueueAbilities(Spell spell) {
        base.QueueAbilities(spell);
        spell.OnArriveEffects.Add(new BallEmbiggener());
        return true;
    }
}
