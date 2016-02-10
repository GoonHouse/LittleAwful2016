using UnityEngine;
using System.Collections;

public enum BallRestlessness{
    Normal,
    Puffing,
    StoppingPuff,
    DidFuckOff,
}

public class Restlessness : MonoBehaviour {
    public float nopeSpeedFactor = 1.25f;

    public int howAccurateToRound = 1;

    public BallRestlessness restlessness = BallRestlessness.Normal;
    public float noChangeTime = 2.0f;
    public float noChangeWarnTime = 1.0f;
    public Vector2 minMoveSpeed = new Vector2(0.05f, 0.05f);
    public Vector3 timerUnchanged = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 lastPos;
    public GameObject burstEffect;
    public bool isWaitingToDisableParticles;
    public float changeDelay;
    public Color preAngerColor;

    public AudioSource nope;

    // general
    Rigidbody2D rigid;

    public virtual void AccumulateStaleness(){
        // Stale Direction Logic
        // Stale flag.
        var deltaTimePayload = new Vector3(0.0f, 0.0f);
        if (God.Round(lastPos.x, howAccurateToRound) == God.Round(transform.localPosition.x, howAccurateToRound)) {
            deltaTimePayload.x = Time.fixedDeltaTime;
        }
        if (God.Round(lastPos.y, howAccurateToRound) == God.Round(transform.localPosition.y, howAccurateToRound)) {
            deltaTimePayload.y = Time.fixedDeltaTime;
        }
        timerUnchanged += deltaTimePayload;
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();

        changeDelay = noChangeTime - noChangeWarnTime;

        FuckOff(1.0f);
    }

    public virtual bool IsTimeToWarn(){
        return (timerUnchanged.x > noChangeWarnTime) || (timerUnchanged.y > noChangeWarnTime);
    }

    public virtual bool IsTimeToFuckOff(){
        return (timerUnchanged.x > noChangeTime) || (timerUnchanged.y > noChangeTime);
    }

    public virtual void FuckOff(float howFast = 1.0f, bool doShout = false) {
        var sr = GetComponent<SpriteRenderer>();
        sr.color = preAngerColor;

        if (doShout) {
            //God.main.Speak(gameObject, "NOPE");
            nope.Play();
        }

        timerUnchanged.x = 0.0f;
        timerUnchanged.y = 0.0f;

        // Get a random angle.
        /*
        Vector2 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;
        v.x *= rigid.velocity.normalized.x;
        v.y *= rigid.velocity.normalized.y;

        rigid.AddForce(v * nopeSpeedFactor);
        */
        
        
        // Get a random angle.
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;

        rigid.AddForce(v * 300.0f);
        

        restlessness = BallRestlessness.Normal;
    }
    
    // Update is called once per frame
    void FixedUpdate () {

        if (restlessness == BallRestlessness.Normal && IsTimeToWarn()) {
            Debug.Log("adh'em DEES");
            restlessness = BallRestlessness.StoppingPuff;
            var sr = GetComponent<SpriteRenderer>();
            preAngerColor = sr.color;
            sr.color = God.redFull;
            changeDelay = noChangeTime - noChangeWarnTime;
            Debug.Log("WHOA BURSTS!");
            var g = God.SpawnAt(burstEffect, transform.position);
            Destroy(g, changeDelay);
        }

        if (restlessness == BallRestlessness.StoppingPuff && IsTimeToFuckOff()){
            Debug.Log("adderall ruined my life");
            FuckOff(1.0f, true);
        }

        AccumulateStaleness();

        // Update last logical position for comparison.
        lastPos = transform.position;
    }
}
