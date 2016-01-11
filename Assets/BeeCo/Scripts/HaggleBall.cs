using UnityEngine;
using System.Collections;

public class HaggleBall : MonoBehaviour {
    public float speed = 30.0f;
    public float noChangeTime = 3.0f;

    public float comboTimer = 0.0f;
    public float minComboTimeToExtend = 0.5f;
    public float maxComboTimeToExtend = 1.0f;

    public int combo = 0;
    public int maxCombo = 15;

    public float forceDownscale = 10000.0f;

    public float ballMinSpeed = 5.0f;
    public float ballMaxSpeed = 25.0f;

    public bool willFuckOff = false;

    public Vector2 minMoveSpeed = new Vector2(0.05f, 0.05f);

    public GameObject hitTextPrefab;

    private Vector3 timerUnchanged = new Vector3(0.0f, 0.0f, 0.0f);

    private Vector3 lastPos;

    public GameObject whoMadeMe;

    Rigidbody2D rigid;

    public float magnetizeForce = 30000.0f;
    private bool isMagnetized = false;
    private GameObject magnetizeTarget;

    void OnDestroy() {
        var paddle = whoMadeMe.GetComponent<Paddle>();
        paddle.BallGone(gameObject);
    }

    private float scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax){
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
    }

    public void MagnetizeTowards(GameObject mt) {
        isMagnetized = true;
        magnetizeTarget = mt;
    }

    public void Demagnetize() {
        isMagnetized = false;
        magnetizeTarget = null;
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        FuckOff(speed);
    }

    void CancelCombo(){
        comboTimer = 0.0f;
        combo = 0;
    }

    static float Round(float value, int digits) {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    void FixedUpdate() {
        if (God.haggleLogic.IsRoundActive()) {
            // Ensure we're active.
            if (rigid.constraints == RigidbodyConstraints2D.FreezeAll) {
                Debug.Log("BALL UNFREEZING SELF");
                rigid.constraints = RigidbodyConstraints2D.None;
            }

            // Combo logic.
            if (comboTimer > 0) {
                comboTimer -= Time.deltaTime;
            } else {
                CancelCombo();
            }

            // Stale Direction Logic
            // Stale flag.
            var deltaTimePayload = new Vector3(0.0f, 0.0f);
            if (Round(lastPos.x, 2) == Round(transform.localPosition.x, 2)) {
                deltaTimePayload.x = Time.deltaTime;
            }
            if (Round(lastPos.y, 2) == Round(transform.localPosition.y, 2)) {
                deltaTimePayload.y = Time.deltaTime;
            }
            timerUnchanged += deltaTimePayload;

            if (timerUnchanged.x > noChangeTime || timerUnchanged.y > noChangeTime) {
                var hitText = (GameObject)Instantiate(hitTextPrefab, transform.position, Quaternion.identity);
                hitText.GetComponent<FloatTextAway>().SetText("FUCK BEES");
                FuckOff();
            }

            // Stay within min/max speed.
            if (rigid.velocity.magnitude < ballMinSpeed) {
                rigid.velocity = rigid.velocity.normalized * ballMinSpeed;
            } else if (rigid.velocity.magnitude > ballMaxSpeed) {
                rigid.velocity = rigid.velocity.normalized * ballMaxSpeed;
            }

            // Magnetization
            if( isMagnetized && magnetizeTarget) {
                var tPos = magnetizeTarget.transform.position;
                rigid.AddForce(Vector3.Normalize(tPos - transform.position) * magnetizeForce);
            }

            // Update last logical position for comparison.
            lastPos = transform.position;
        } else {
            Debug.Log("BALLS FROZEN");
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("space")) {
            FuckOff();
        }
    }

    void BumpCombo(){
        combo++;
        comboTimer = scale(Mathf.Min(combo, maxCombo), 0, maxCombo, maxComboTimeToExtend, minComboTimeToExtend);
    }

    public void GetHurt(float howMuch = 1.0f) {
        var comboRatio = (((combo+1) * 2) / 100.0f) * 2;
        var fuckAmount = (howMuch / forceDownscale) * combo * 2;

        combo = 0;
        comboTimer = 0.0f;

        Camera.main.GetComponent<ShakeCamera>().Jostle(fuckAmount);
        var pos = transform.position;
        pos.z = -20.0f;
        var hitText = (GameObject)Instantiate(hitTextPrefab, pos, Quaternion.identity);
        hitText.GetComponent<FloatTextAway>().SetMoney(-fuckAmount);

        God.haggleLogic.adjustPrice(+fuckAmount);
    }

    public static float KineticEnergy(Rigidbody2D rb) {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.CompareTag("Brick")) {
            coll.gameObject.SendMessage("TakeDamage");
            BumpCombo();
            var comboRatio = Mathf.Min((float)combo / (float)maxCombo, 1.0f);
            Camera.main.GetComponent<ShakeCamera>().Jostle(comboRatio);

            var priceDrop = comboRatio + (KineticEnergy(GetComponent<Rigidbody2D>()) / forceDownscale);

            var pos = coll.gameObject.transform.position;
            pos.z = -20.0f;
            var hitText = (GameObject)Instantiate(hitTextPrefab, pos, Quaternion.identity);
            hitText.GetComponent<FloatTextAway>().SetMoney(priceDrop);

            God.haggleLogic.adjustPrice(-priceDrop);

            // this.GetComponent<healthScript>().health -= 1;
        } else if(coll.gameObject.CompareTag("HurtBall")){
            GetHurt( KineticEnergy(GetComponent<Rigidbody2D>()) );
        } else if( !coll.gameObject.CompareTag("Player") && willFuckOff ) {
            Debug.Log("Initiating fuckoff.");
            FuckOff();
        }
    }

    void FuckOff(float howFast = 1.0f){
        //Debug.Log("FUCKING OFF!!!");
        willFuckOff = false;
        timerUnchanged.x = 0.0f;
        timerUnchanged.y = 0.0f;

        // Get a random angle.
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;

        rigid.AddForce(v * speed);
    }
}
