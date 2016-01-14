using UnityEngine;
using System.Collections;

public class HaggleBall : MonoBehaviour {
    public float speed = 30.0f;

    public float forceDownscale = 10000.0f;

    public float ballMinSpeed = 5.0f;
    public float ballMaxSpeed = 25.0f;

    public GameObject hitTextPrefab;
    public ParticleSystem burstEffect;

    // Damage
    public float damageDone = 1.0f;

    // Restlessness Timer
    public float noChangeTime = 3.0f;
    public float noChangeWarnTime = 2.0f;
    public Vector2 minMoveSpeed = new Vector2(0.05f, 0.05f);
    private Vector3 timerUnchanged = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 lastPos;
    private bool isWaitingToDisableParticles;
    private float changeDelay;

    // Gravitation
    public bool isGravitized = false;

    // Magnetization
    public float magnetizeForce = 30000.0f;
    private bool isMagnetizedTowards = false;
    private bool isMagnetized = false;
    private GameObject magnetizeTarget;

    // Other stuff.
    public GameObject whoMadeMe;
    public Paddle paddle;
    Rigidbody2D rigid;

    void OnDestroy() {
        if( whoMadeMe != null) {
            // Don't care!
            paddle.BallGone(gameObject);
        }
    }

    private float scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax){
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
    }

    public void Gravitize() {
        isGravitized = true;
        rigid.gravityScale = 1;
        rigid.mass *= 50.0f;
        var coll = GetComponent<CircleCollider2D>();
        coll.sharedMaterial.bounciness = 0;
        coll.sharedMaterial.friction = 1;
        rigid.velocity = Vector3.zero;
    }

    public void Degravitize() {
        isGravitized = false;
        rigid.gravityScale = 0;
        rigid.mass /= 50.0f;
        var coll = GetComponent<CircleCollider2D>();
        coll.sharedMaterial.bounciness = 1;
        coll.sharedMaterial.friction = 0;
    }

    public void Magnetize(GameObject mt, bool towards = true) {
        isMagnetized = true;
        magnetizeTarget = mt;
        isMagnetizedTowards = towards;
    }

    public void Demagnetize() {
        isMagnetized = false;
        magnetizeTarget = null;
        isMagnetizedTowards = false;
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        paddle = whoMadeMe.GetComponent<Paddle>();
        changeDelay = noChangeTime - noChangeWarnTime;
        FuckOff(speed);
    }

    static float Round(float value, int digits) {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    public void SpeakMoney(float amount) {
        var hitText = (GameObject)Instantiate(hitTextPrefab, transform.position, Quaternion.identity);
        hitText.GetComponent<FloatTextAway>().SetMoney(amount);
    }

    public void Speak(string text) {
        var hitText = (GameObject)Instantiate(hitTextPrefab, transform.position, Quaternion.identity);
        hitText.GetComponent<FloatTextAway>().SetText(text);
    }

    IEnumerator StopEffect() {
        if( isWaitingToDisableParticles ) {
            yield return new WaitForSeconds(changeDelay);
            isWaitingToDisableParticles = false;
            burstEffect.Stop();
        }
    }

    void FixedUpdate() {
        if (God.haggleLogic.IsRoundActive()) {
            // Ensure we're active.
            if (rigid.constraints == RigidbodyConstraints2D.FreezeAll) {
                rigid.constraints = RigidbodyConstraints2D.None;
            }

            if ( !isGravitized ) {
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

                if (!isWaitingToDisableParticles && (timerUnchanged.x > noChangeWarnTime || timerUnchanged.y > noChangeWarnTime)) {
                    changeDelay = noChangeTime - noChangeWarnTime;
                    burstEffect.Play();
                    isWaitingToDisableParticles = true;
                    StartCoroutine("StopEffect");
                }

                if (timerUnchanged.x > noChangeTime || timerUnchanged.y > noChangeTime) {
                    FuckOff(1.0f, true);
                }

                // Stay within min/max speed.
                if (rigid.velocity.magnitude < ballMinSpeed) {
                    rigid.velocity = rigid.velocity.normalized * ballMinSpeed;
                } else if (rigid.velocity.magnitude > ballMaxSpeed) {
                    rigid.velocity = rigid.velocity.normalized * ballMaxSpeed;
                }
            } else {
                Debug.Log("gravitized");
            }

            // Magnetization
            if( isMagnetized && magnetizeTarget) {
                var tPos = magnetizeTarget.transform.position;
                Vector3 magDirection;
                if( isMagnetizedTowards) {
                    magDirection = Vector3.Normalize(tPos - transform.position);
                } else {
                    magDirection = Vector3.Normalize(transform.position - tPos);
                }
                rigid.AddForce(magDirection * magnetizeForce);
            }

            // Update last logical position for comparison.
            lastPos = transform.position;
        } else {
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void GetHurt(float howMuch = 1.0f) {
        var priceIncrease = ( paddle.GetComboRatio(false) + (KineticEnergy(rigid) / forceDownscale) ) * 2;

        whoMadeMe.GetComponent<Paddle>().CancelCombo();

        Camera.main.GetComponent<ShakeCamera>().Jostle(priceIncrease);
        var pos = transform.position;
        pos.z = -20.0f;
        SpeakMoney(-priceIncrease);

        God.haggleLogic.AdjustPrice(priceIncrease);
    }

    public static float KineticEnergy(Rigidbody2D rb) {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }

    void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.CompareTag("Brick")) {
            var healthLeft = coll.gameObject.GetComponent<BrickScript>().TakeDamage(damageDone);
            paddle.BumpCombo();

            var comboRatio = paddle.GetComboRatio();

            Camera.main.GetComponent<ShakeCamera>().Jostle(comboRatio);

            var priceDrop = comboRatio + (KineticEnergy(rigid) / forceDownscale);

            var pos = coll.gameObject.transform.position;
            pos.z = -20.0f;
            SpeakMoney(priceDrop);

            God.haggleLogic.AdjustPrice(-priceDrop);

            // this.GetComponent<healthScript>().health -= 1;
        } else if(coll.gameObject.CompareTag("HurtBall")){
            GetHurt( KineticEnergy(GetComponent<Rigidbody2D>()) );
        }
    }

    public void FuckOff(float howFast = 1.0f, bool doShout = false){
        if( doShout) {
            Speak("FUCK BEES");
        }

        timerUnchanged.x = 0.0f;
        timerUnchanged.y = 0.0f;

        // Get a random angle.
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;

        rigid.AddForce(v * speed);
    }
}
