using UnityEngine;
using System.Collections;

public class GirthMaintainer : MonoBehaviour {
    public float maxScaleRatio = 2.0f;

    private TrailRenderer tr;

    // Use this for initialization
    void Start() {
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        tr.startWidth = transform.parent.localScale.x * maxScaleRatio;
    }
}
