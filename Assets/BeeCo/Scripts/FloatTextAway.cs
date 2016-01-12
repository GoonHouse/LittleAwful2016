using UnityEngine;
using System.Collections;

public class FloatTextAway : MonoBehaviour {
    public float floatAwaySpeed = 3.0f;
    public float colorLerpTime = 1.0f;
    public float startLifeTime = 1.0f;

    public float lifeTime;

    private float colorLerp = 0.0f;
    private Color startColor;
    private Color endColor;

    public Color badColor = new Color(255.0f, 1.0f, 1.0f, 255.0f);
    public Color farBadColor = new Color(255.0f, 1.0f, 1.0f, 10.0f);
    public Color goodColor = new Color(1.0f, 255.0f, 1.0f, 255.0f);
    public Color farGoodColor = new Color(1.0f, 255.0f, 1.0f, 10.0f);


    // Use this for initialization
    void Start () {
        lifeTime = startLifeTime;
	}

    public void SetText(string text, bool feelGood = false) {
        var tm = GetComponent<TextMesh>();

        if ( feelGood ) {
            startColor = goodColor;
            endColor = farGoodColor;
        } else {
            startColor = badColor;
            endColor = farBadColor;
        }

        tm.text = text;
    }

    public void SetMoney(float number) {
        var tm = GetComponent<TextMesh>();
        var prefix = "";
        
        if( number > 0) {
            prefix = "-";
            startColor = goodColor;
            endColor = farGoodColor;
        } else {
            prefix = "+";
            startColor = badColor;
            endColor = farBadColor;
        }

        tm.text = prefix + "$" + Mathf.Abs(number).ToString("F2");
    }

	// Update is called once per frame
	void Update () {
        // Time to live.
        lifeTime -= Time.deltaTime;

        // Move up.
        var pos = transform.localPosition;
		pos.y += Time.deltaTime * floatAwaySpeed + (startLifeTime - lifeTime) * 0.1f;
        transform.localPosition = pos;

        

        // Play with colors.
        colorLerp += Time.deltaTime;
        var tm = GetComponent<TextMesh>();
        tm.color = Color.Lerp(startColor, endColor, colorLerp / colorLerpTime);
        
        if( lifeTime <= 0.0f) {
            Destroy(gameObject);
        }
    }
}
