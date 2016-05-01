using UnityEngine;
using System.Collections;

public class CameraRectifier : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        // why do these numbers work? I don't fuckin know.
        Camera.main.orthographicSize = God.Scale(Camera.main.aspect, 5.0f / 4.0f, 16.0f / 9.0f, 7.2f, 5.0f);
	}
}
