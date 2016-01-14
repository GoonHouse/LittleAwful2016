using UnityEngine;
using System;
using System.Collections;

/*
    While most people will discourage the use of god objects, this one exists so that
    everything doesn't get trashed between scenes and important things can be found always.
*/
public class God : MonoBehaviour {
    public static God main;
    public static HaggleLogic haggleLogic;
    public static LevelTransiton levelTransition;
    public static PlayerStats playerStats;

    // very important prefab
    public GameObject hitText;

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
        haggleLogic = GetComponent<HaggleLogic>();
        levelTransition = GetComponent<LevelTransiton>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update() {

    }

    // helpful public methods because why not
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

}