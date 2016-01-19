using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/*
    While most people will discourage the use of god objects, this one exists so that
    everything doesn't get trashed between scenes and important things can be found always.
*/
public class God : MonoBehaviour {
    public static God main;
    public static HaggleLogic haggleLogic;
    public static LevelTransiton levelTransition;
    public static PlayerStats playerStats;
    public static TopSecret topSecret;

    public static Color redFull = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    public static Color greenFull = new Color(0.0f, 1.0f, 0.0f, 1.0f);

    // very important prefab
    public GameObject hitText;
    public GameObject bird;

    // don't worry
    public int maxNumSignals = 100;
    public List<string> holySignals;

    // game object stuff
    void Awake() {
        if (main == null) {
            DontDestroyOnLoad(gameObject);
            main = this;
        } else if (main != this) {
            DestroyImmediate(gameObject);
        }
    }

    // Use this for initialization
    void Start() {
        Application.runInBackground = true;

        holySignals = new List<string>();
        haggleLogic = GetComponent<HaggleLogic>();
        levelTransition = GetComponent<LevelTransiton>();
        playerStats = GetComponent<PlayerStats>();
        topSecret = GetComponent<TopSecret>();
        topSecret.messageRecievedEvent.AddListener(HandleHolySignal);
    }

    // Update is called once per frame
    void Update() {
        if( Input.GetKeyDown("h")) {
            Bird("hail satan");
        }
    }

    public void Bird(string msg) {
        if (SceneManager.GetActiveScene().name != "breakout" &&
            SceneManager.GetActiveScene().name != "title" &&
            SceneManager.GetActiveScene().name != "victory") {

            var pos = levelTransition.FindPlayer().transform.position;
            pos.x += 5;
            pos.y -= 13;
            var b = SpawnAt(bird, pos);
            var bs = b.GetComponent<BirdScript>();
            bs.startPos = pos;
            b.transform.position = pos;
            pos.y += 10;
            bs.endPos = pos;
            bs.words.text = msg;
        }
    }

    // helpful public methods because why not
    public static GameObject SpawnChild(GameObject go, GameObject self, bool extra = true) {
        var i = Instantiate(go, self.transform.position, Quaternion.identity) as GameObject;
        i.transform.SetParent(self.transform, extra);
        return i;
    }

    public static GameObject SpawnAt(GameObject go, Vector3 pos) {
        return Instantiate(go, pos, Quaternion.identity) as GameObject;
    }

    public GameObject SpeakMoney(GameObject self, float amount) {
        var ht = SpawnAt(hitText, self.transform.position);
        ht.GetComponent<FloatTextAway>().SetMoney(amount);
        return ht;
    }

    public GameObject Speak(GameObject self, string text) {
        var ht = SpawnAt(hitText, self.transform.position);
        ht.GetComponent<FloatTextAway>().SetText(text);
        return ht;
    }

    void HandleHolySignal( string msg ) {
        //parse from buffer.
        int msgIndex = msg.IndexOf("PRIVMSG #");
        string msgString = msg.Substring(msgIndex + topSecret.channelName.Length + 11);
        string user = msg.Substring(1, msg.IndexOf('!') - 1);

        //remove old messages for performance reasons.
        if( holySignals.Count > maxNumSignals ){
            holySignals.RemoveAt(0);
        }

        //add new message.
        holySignals.Add(user + ": " + msgString);

        Bird(user + ": " + msgString);
    }

    public string PopSignal() {
        if (holySignals.Count > 0 ) {
            var s = holySignals[0];
            holySignals.RemoveAt(0);
            return s;
        } else {
            return "";
        }
    }

    public static string FormatMoney(float amount, bool showPlus = false) {
        var prefix = "";

        if (amount < 0) {
            prefix = "-";
        } else if( amount > 0 && showPlus) {
            prefix = "+";
        }

        return prefix + "$" + Mathf.Abs(amount).ToString("F2");
    }

    public static Color ColorOfMoney(float amount) {
        if( amount < 0.0f ) {
            return new Color(1.0f, 0.0f, 0.0f, 1.0f);
        } else {
            return new Color(0.0f, 1.0f, 0.0f, 1.0f);
        }
    }

    public static string FormatTime(float time) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    public static float Scale(float valueIn, float baseMin, float baseMax, float limitMin, float limitMax) {
        return ((limitMax - limitMin) * (valueIn - baseMin) / (baseMax - baseMin)) + limitMin;
    }

    public static float KineticEnergy(Rigidbody2D rb) {
        // mass in kg, velocity in meters per second, result is joules
        return 0.5f * rb.mass * Mathf.Pow(rb.velocity.magnitude, 2);
    }

    public static float Round(float value, int digits) {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
}