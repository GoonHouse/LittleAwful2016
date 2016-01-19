using UnityEngine;
using System.Collections;

public enum BallRestlessness {
    Normal,
    Puffing,
    StoppingPuff,
    DidFuckOff,
}

public class HaggleBall : MonoBehaviour {
    public float speed = 30.0f;

    public float forceDownscale = 10000.0f;

    public float ballMinSpeed = 5.0f;
    public float ballMaxSpeed = 25.0f;

    // slow motion
    public bool isSlowMotion = false;
    public float ballPrevVelocity;
    public float slowMoFactor = 2.0f;

    public GameObject hitTextPrefab;
    public GameObject burstEffect;

    public AudioSource brickhit;
    public AudioSource paddlehit;
    public AudioSource nope;
    public AudioSource hurtsound;

    // Damage
    public float damageDone = 1.0f;

    #region // Gravitation
    public bool isGravitized = false;
    public float baseGravitizationTime = 3.0f;
    public float gravitizationTime;
    #endregion

    #region // Magnetization
    public float magnetizeForce = 30000.0f;
    public bool isMagnetizedTowards = false;
    public bool isMagnetized = false;
    public GameObject magnetizeTarget;
    #endregion

    // Other stuff.
    public GameObject whoMadeMe;
    public Paddle paddle;
    Rigidbody2D rigid;

    public virtual void OnDestroy() {
        if (whoMadeMe != null) {
            // Don't care!
            paddle.BallGone(gameObject);
        }
    }

    public virtual float Gravitize(float strength = 1.0f) {
        isGravitized = true;
        gravitizationTime += strength * baseGravitizationTime;
        rigid.gravityScale = 1;
        rigid.mass += 50.0f;
        var coll = GetComponent<CircleCollider2D>();
        coll.sharedMaterial.bounciness = 0;
        coll.sharedMaterial.friction = 1;
        rigid.velocity = Vector3.zero;
        return gravitizationTime;
    }

    public virtual void Degravitize() {
        isGravitized = false;
        gravitizationTime = 0.0f;
        rigid.gravityScale = 0;
        rigid.mass -= 50.0f;
        var coll = GetComponent<CircleCollider2D>();
        coll.sharedMaterial.bounciness = 1;
        coll.sharedMaterial.friction = 0;
    }

    public virtual void Magnetize(GameObject mt, bool towards = true) {
        isMagnetized = true;
        magnetizeTarget = mt;
        isMagnetizedTowards = towards;
    }

    public virtual void Demagnetize() {
        isMagnetized = false;
        magnetizeTarget = null;
        isMagnetizedTowards = false;
    }

    public virtual void SlowMotion() {
        isSlowMotion = true;
        ballPrevVelocity = rigid.velocity.magnitude;
        ballMinSpeed /= slowMoFactor;
        ballMaxSpeed /= slowMoFactor;
    }

    public virtual void NormalMotion() {
        isSlowMotion = false;
        ballMinSpeed *= slowMoFactor;
        ballMaxSpeed *= slowMoFactor;
        rigid.velocity = rigid.velocity.normalized * ballPrevVelocity;
        ballPrevVelocity = 0.0f;
    }

    // Restlessness Timer
    public BallRestlessness restlessness = BallRestlessness.Normal;
    public float noChangeTime = 3.0f;
    public float noChangeWarnTime = 2.0f;
    public Vector2 minMoveSpeed = new Vector2(0.05f, 0.05f);
    public Vector3 timerUnchanged = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector3 lastPos;
    public bool isWaitingToDisableParticles;
    public float changeDelay;
    public Color preAngerColor;

    // Use this for initialization
    public virtual void Start () {
        rigid = GetComponent<Rigidbody2D>();
        if( whoMadeMe ) {
            paddle = whoMadeMe.GetComponent<Paddle>();
        }
        changeDelay = noChangeTime - noChangeWarnTime;
        FuckOff(speed);
    }

    public virtual void SpeakMoney(float amount) {
        var hitText = (GameObject)Instantiate(hitTextPrefab, transform.position, Quaternion.identity);
        hitText.GetComponent<FloatTextAway>().SetMoney(amount);
    }

    public virtual void Speak(string text) {
        var hitText = (GameObject)Instantiate(hitTextPrefab, transform.position, Quaternion.identity);
        hitText.GetComponent<FloatTextAway>().SetText(text);
    }

    public virtual void AccumulateStaleness() {
        // Stale Direction Logic
        // Stale flag.
        var deltaTimePayload = new Vector3(0.0f, 0.0f);
        if (God.Round(lastPos.x, 2) == God.Round(transform.localPosition.x, 2)) {
            deltaTimePayload.x = Time.fixedDeltaTime;
        }
        if (God.Round(lastPos.y, 2) == God.Round(transform.localPosition.y, 2)) {
            deltaTimePayload.y = Time.fixedDeltaTime;
        }
        timerUnchanged += deltaTimePayload;
    }

    public virtual bool IsTimeToWarn() {
        return (timerUnchanged.x > noChangeWarnTime) || (timerUnchanged.y > noChangeWarnTime);
    }

    public virtual bool IsTimeToFuckOff() {
        return (timerUnchanged.x > noChangeTime) || (timerUnchanged.y > noChangeTime);
    }

    public virtual void FixedUpdate() {
        if (God.haggleLogic.IsRoundActive()) {
            // Ensure we're active.
            if (rigid.constraints == RigidbodyConstraints2D.FreezeAll) {
                rigid.constraints = RigidbodyConstraints2D.None;
            }

            if ( gravitizationTime <= 0.0f ) {
                if( isGravitized) {
                    Degravitize();
                }

                AccumulateStaleness();

                if( restlessness == BallRestlessness.Normal && IsTimeToWarn() ) {
                    restlessness = BallRestlessness.StoppingPuff;
                    var sr = GetComponent<SpriteRenderer>();
                    preAngerColor = sr.color;
                    sr.color = God.redFull;
                    changeDelay = noChangeTime - noChangeWarnTime;
                    Debug.Log("WHOA BURSTS!");
                    var g = God.SpawnAt(burstEffect, transform.position);
                    Destroy(g, changeDelay);
                }

                if ( restlessness == BallRestlessness.StoppingPuff && IsTimeToFuckOff() ) {
                    FuckOff(1.0f, true);
                }

                // Stay within min/max speed.
                if (rigid.velocity.magnitude < ballMinSpeed) {
                    rigid.velocity = rigid.velocity.normalized * ballMinSpeed;
                } else if (rigid.velocity.magnitude > ballMaxSpeed) {
                    rigid.velocity = rigid.velocity.normalized * ballMaxSpeed;
                }
            } else {
                gravitizationTime -= Time.fixedDeltaTime;
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
    public virtual void Update() {

    }

    public virtual void GetHurt(float howMuch = 1.0f) {
        if( paddle) {
            var priceIncrease = (paddle.GetComboRatio(false) + (God.KineticEnergy(rigid) / forceDownscale)) * 2;

            whoMadeMe.GetComponent<Paddle>().CancelCombo();

            Camera.main.GetComponent<ShakeCamera>().Jostle(priceIncrease);
            var pos = transform.position;
            pos.z = -20.0f;
            SpeakMoney(-priceIncrease);

            God.haggleLogic.AdjustPrice(priceIncrease);
            hurtsound.Play();
        }
        
    }

    public virtual void OnCollisionEnter2D(Collision2D coll){
        if (coll.gameObject.CompareTag("Brick")) {
            var healthLeft = coll.gameObject.GetComponent<BrickScript>().TakeDamage(damageDone);
            var comboRatio = healthLeft;
            brickhit.Play();
            if( paddle ) {
                paddle.BumpCombo();
                comboRatio = paddle.GetComboRatio();
            }
            
            Camera.main.GetComponent<ShakeCamera>().Jostle(comboRatio);

            var priceDrop = comboRatio + (God.KineticEnergy(rigid) / forceDownscale);

            var pos = coll.gameObject.transform.position;
            pos.z = -20.0f;
            SpeakMoney(priceDrop);

            God.haggleLogic.AdjustPrice(-priceDrop);

            // this.GetComponent<healthScript>().health -= 1;
        } else if(coll.gameObject.CompareTag("HurtBall")){
            GetHurt( God.KineticEnergy(GetComponent<Rigidbody2D>()) );
        } else if(coll.gameObject.CompareTag("Player"))
        {
            paddlehit.Play();
        }
    }

    public virtual void FuckOff(float howFast = 1.0f, bool doShout = false){
        if( preAngerColor != null ) {
            var sr = GetComponent<SpriteRenderer>();
            sr.color = preAngerColor;
        }

        if ( doShout ) {
            Speak("NOPE");
            nope.Play();
        }

        timerUnchanged.x = 0.0f;
        timerUnchanged.y = 0.0f;

        // Get a random angle.
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;

        rigid.AddForce(v * speed);
        restlessness = BallRestlessness.Normal;
    }
}
