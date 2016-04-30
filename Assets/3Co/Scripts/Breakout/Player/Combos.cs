using UnityEngine;
using System.Collections;

public class Combos : MonoBehaviour {
    public float comboTimer = 0.0f;
    public float minComboTimeToExtend = 0.5f;
    public float maxComboTimeToExtend = 1.0f;

    public int combo = 0;
    public int maxCombo = 15;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (comboTimer > 0) {
            comboTimer -= Time.deltaTime;
        } else {
            CancelCombo();
        }
    }

    // Combo Stuff
    public void CancelCombo() {
        comboTimer = 0.0f;
        combo = 0;
    }

    public void BumpCombo() {
        combo++;
        comboTimer = God.Scale(Mathf.Min(combo, maxCombo), 0, maxCombo, maxComboTimeToExtend, minComboTimeToExtend);
    }

    public float GetComboRatio(bool limit = true) {
        var comboRatio = (float)combo / (float)maxCombo;
        if (limit) {
            comboRatio = Mathf.Min(comboRatio, 1.0f);
        }
        return comboRatio;
    }
}
