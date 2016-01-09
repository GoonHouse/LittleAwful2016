using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	public float smoothing;
	private float[] parallaxScales;
	private Transform camera;
	private Vector3 previousCameraPosition;

	// Use this for initialization
	void Start () {
		camera = Camera.main.transform;
		previousCameraPosition = camera.position;
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * -1;
		}	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCameraPosition.x - camera.position.x) * parallaxScales [i];
			float backgroundTargetPositionX = backgrounds [i].position.x + parallax;
			Vector3 backgroundTargetPosition = new Vector3 (backgroundTargetPositionX, backgrounds [i].position.y, backgrounds [i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPosition, smoothing * Time.deltaTime);		
		}
		previousCameraPosition = camera.position;
	}
}
