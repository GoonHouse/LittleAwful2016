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

    public HaggleLogic haggleLogic;

    public bool willFuckOff = false;

    public Vector2 minMoveSpeed = new Vector2(0.05f, 0.05f);

    private Vector3 timerUnchanged = new Vector3(0.0f, 0.0f, 0.0f);

    private Vector3 lastPos;

    Rigidbody2D rigid;

    private float scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax){
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
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
        if (comboTimer > 0) {
            comboTimer -= Time.deltaTime;
        } else {
            CancelCombo();
        }

        // Stale flag.
        var deltaTimePayload = new Vector3(0.0f, 0.0f);
        if (Round(lastPos.x,2) == Round(transform.localPosition.x,2)) {
            deltaTimePayload.x = Time.deltaTime;
        }
        if (Round(lastPos.y,2) == Round(transform.localPosition.y,2)) {
            deltaTimePayload.y = Time.deltaTime;
        }
        timerUnchanged += deltaTimePayload;

        if (timerUnchanged.x > noChangeTime || timerUnchanged.y > noChangeTime) {
            willFuckOff = true;
        }

        /*
            Dear Future EntranceJew,

            You were thinking of subclassing this and making another ball that heals blocks.
            Additionally, you were going to punch Danny in his everything.

        */

        lastPos = transform.position;
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

    void OnCollisionEnter2D(Collision2D coll){
        if( coll.gameObject.CompareTag("Brick") ){
            coll.gameObject.SendMessage("TakeDamage");
            BumpCombo();
            var comboRatio = Mathf.Min( (float)combo / (float)maxCombo, 1.0f );
            Camera.main.GetComponent<ShakeCamera>().Jostle( comboRatio );
            haggleLogic.adjustPrice( -comboRatio );

            // this.GetComponent<healthScript>().health -= 1;
        } else if( !coll.gameObject.CompareTag("Player") && willFuckOff ) {
            Debug.Log("Initiating fuckoff.");
            //FuckOff();
        }
    }

    void FuckOff(float howFast = 1.0f){
        Debug.Log("FUCKING OFF!!!");
        willFuckOff = false;
        timerUnchanged.x = 0.0f;
        timerUnchanged.y = 0.0f;

        // Get a random angle.
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;
        var vel = rigid.velocity;

        // Ensure we are moving at all.
        if ( Mathf.Abs(vel.x) <= minMoveSpeed.x ){
            vel.x = v.x * minMoveSpeed.x;
        }
        if (Mathf.Abs(vel.y) <= minMoveSpeed.y ) {
            vel.y = v.y * minMoveSpeed.y;
        }

        // Pump up our speed.
        vel.x *= v.x * howFast;
        vel.y *= v.y * howFast;

        // Get the show on the road.
        rigid.velocity = vel;
    }
}
