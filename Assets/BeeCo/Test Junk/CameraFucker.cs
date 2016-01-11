using UnityEngine;
using System.Collections;

public class CameraFucker : MonoBehaviour {
	public float H = 0.0f;
	public float V = 0.0f;

	// Use this for initialization
	void Start () {
		SetObliqueness (H, V);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetObliqueness(float horizObl, float vertObl) {
		Matrix4x4 mat  = Camera.main.projectionMatrix;
		mat[0, 2] = horizObl;
		mat[1, 2] = vertObl;
		Camera.main.projectionMatrix = mat;
	}
}
