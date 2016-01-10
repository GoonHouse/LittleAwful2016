using UnityEngine;
using System.Collections;

public class FloatTextAway : MonoBehaviour {
    public float floatAwaySpeed = 3.0f;
    public float colorLerpTime = 1.0f;

    private float colorLerp = 0.0f;
    private Color startColor;
    private Color endColor;

    public Color badColor = new Color(255, 0, 0);
    public Color goodColor = new Color(0, 255, 0);
    public Color baseColor = new Color(0, 0, 255);

    // Use this for initialization
    void Start () {
         
	}

    public void SetMoney(float number) {
        var tm = GetComponent<TextMesh>();
        var prefix = "";
        
        if( number > 0) {
            prefix = "-";
            startColor = goodColor;
            endColor = baseColor;
        } else {
            prefix = "+";
            startColor = badColor;
            endColor = baseColor;
        }

        tm.text = prefix + "$" + Mathf.Abs(number).ToString("F2");
    }

	// Update is called once per frame
	void Update () {
        // Move up.
        var pos = transform.localPosition;
        pos.y += Time.deltaTime * floatAwaySpeed;
        transform.localPosition = pos;

        // Play with colors.
        colorLerp += Time.deltaTime;
        var tm = GetComponent<TextMesh>();
        tm.color = Color.Lerp(startColor, endColor, colorLerp / colorLerpTime);

    }
}
