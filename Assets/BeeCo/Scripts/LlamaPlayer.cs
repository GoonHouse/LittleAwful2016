using UnityEngine;
using System.Collections;

public class LlamaPlayer : MonoBehaviour {
    public GameObject hitTextPrefab;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SpeakMoney(float amount) {
        var hitText = (GameObject)Instantiate(hitTextPrefab, transform.position, Quaternion.identity);
        hitText.GetComponent<FloatTextAway>().SetMoney(amount);
    }
}
