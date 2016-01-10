using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelTransiton : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Platformer() {
        SceneManager.LoadScene("platformer");
    }
}
