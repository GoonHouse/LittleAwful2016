using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPool : MonoBehaviour {

    // these are hit noises, we will define these in the editor
    public List<AudioClip> pool;
    public AudioSource source;
    //public GameObject audioGhost;

    // Use this for initialization
    void Start() {
        // get my own audio source, with defined settings on it
        if( source == null) {
            source = GetComponent<AudioSource>();
        }

        /*
        if( audioGhost == null) {
            audioGhost = Resources.Load("Helpers/AudioGhost") as GameObject;
        }
        */
    }

    public void PlayRandom() {
        // get a random clip from the list
        var randClip = pool[Random.Range(0, pool.Count - 1)];
        // let's use the settings already on the Audio Source to play this one clip, presumed
        // to be balanced and equalized with the rest
        // (if it's not we can use the second parameter to PlayOneShot)
        source.PlayOneShot(randClip);
    }
    
    public void PlayRandomGhost() {
        var randClip = pool[Random.Range(0, pool.Count - 1)];
        //var aus = Instantiate(audioGhost, transform.position, Quaternion.identity) as GameObject;
        //aus.GetComponent<AudioSource>().PlayOneShot(randClip);
        //Destroy(gameObject, randClip.length);
    }
}