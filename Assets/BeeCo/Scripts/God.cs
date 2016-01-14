using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    // very important prefab
    public GameObject hitText;

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

    }

    // helpful public methods because why not
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
}