using UnityEngine;
using System.Collections;

public class GirthToggler : MonoBehaviour {
    public float timeToFlash = 3.0f;
    public float currentTime = 0.0f;

    public float maxGirth = 25.0f;
    public float minGirth = 0.0f;

    public float fromGirth;
    public float toGirth;

    public TrailRenderer tr;

    // Use this for initialization
    void Start() {
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        currentTime -= Time.fixedDeltaTime;

        if (currentTime <= 0.0f) {
            toGirth = fromGirth;
            fromGirth = Random.Range(minGirth, maxGirth);

            // adding because tiny difference in negative deltas
            currentTime += timeToFlash;
        }

        tr.startWidth = Mathf.Lerp(fromGirth, toGirth, currentTime / timeToFlash);
    }
}
