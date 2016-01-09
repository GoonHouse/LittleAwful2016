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

    private Vector3 timerUnchanged = new Vector3(0.0f, 0.0f, 0.0f);
    private float timerNoChangeY = 0.0f;

    private Vector3 lastPos;

    Rigidbody2D rigid;

    private float scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax){
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        FuckOff();
    }

    void CancelCombo(){
        comboTimer = 0.0f;
        combo = 0;
    }
    
    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown("space") ){
            FuckOff();
        }

        if( comboTimer > 0){
            comboTimer -= Time.deltaTime;
        } else {
            CancelCombo();
        }

        /*
            Dear Future EntranceJew,

            You were thinking of subclassing this and making another ball that heals blocks.
            Additionally, you were going to punch Danny in his everything.

        */

        //transform.pos
        //if( Mathf.Round(transform.position.x, 2) )
    }

    void BumpCombo(){
        combo++;
        comboTimer = scale(Mathf.Min(combo, maxCombo), 0, maxCombo, maxComboTimeToExtend, minComboTimeToExtend);
    }

    void OnCollisionEnter2D(Collision2D coll){
        if( coll.gameObject.tag == "Brick" ){
            coll.gameObject.SendMessage("TakeDamage");
            BumpCombo();
            Camera.main.GetComponent<ShakeCamera>().Jostle( ((float)combo)/((float)maxCombo) );

            // this.GetComponent<healthScript>().health -= 1;
        }
    }

    void FuckOff(){
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;
        rigid.velocity = v * speed;
    }
}
