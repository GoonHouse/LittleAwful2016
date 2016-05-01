using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class Circle : MonoBehaviour {
    public int segments;
    public float xradius;
    public float yradius;
    public float visualParts = 1.0f;
    LineRenderer line;

    void Start() {
        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
    }

    void Update() {
        line.SetVertexCount(segments + 1);
        CreatePoints();
    }


    void CreatePoints() {
        float angle = 20f;

        Vector3 lastPos = new Vector3();

        for (int i = 0; i < (segments + 1); i++) {
            if( i < Mathf.Floor(visualParts*segments) ) {
                lastPos.x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
                lastPos.y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
                angle += (360f / segments);
            }

            line.SetPosition(i, lastPos);
        }
    }
}