using UnityEngine;
using System.Collections;

public class AIPlaceBricksRandomly : MonoBehaviour {

    public float brickSpawnTimerDelay = 1.0f;
    public float brickSpawnTimer;

    // Use this for initialization
    void Start () {
        brickSpawnTimer = brickSpawnTimerDelay;
    }
	
	// Update is called once per frame
	void Update () {
        brickSpawnTimer -= Time.deltaTime;
        if (brickSpawnTimer <= 0.0f) {
            var absp = gameObject.GetComponent<AbstractPlayer>();
            absp.UseActiveFamiliar(absp.FindAPlaceToSpawn(absp.brick));
            brickSpawnTimer = brickSpawnTimerDelay;
        }
    }
}
