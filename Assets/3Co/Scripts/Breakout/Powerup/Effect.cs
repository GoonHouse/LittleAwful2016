using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Effect {
    public UnityAction isActive;
    public UnityAction shouldEnd;
    public UnityAction onStart;
    public UnityAction onEnd;

    public Effect(UnityAction isActive, UnityAction shouldEnd, UnityAction onStart, UnityAction onEnd) {
        this.isActive = isActive;
        this.shouldEnd = shouldEnd;
        this.onStart = onStart;
        this.onEnd = onEnd;
    }
}
