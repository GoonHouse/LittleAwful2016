using UnityEngine;
using System.Collections;

public class FeignDeath : MonoBehaviour {
    public float delay = 6.0f;

    // Use this for initialization
    public void FakeDestroy() {
        foreach(Renderer renderer in gameObject.GetComponents<Renderer>()) {
            Debug.Log(renderer);
            renderer.enabled = false;
        }
        foreach (Collider collider in gameObject.GetComponents<Collider>()) {
            Debug.Log(collider);
            collider.enabled = false;
        }
        foreach (Collider2D collider in gameObject.GetComponents<Collider2D>()) {
            Debug.Log(collider);
            collider.enabled = false;
        }
        foreach (AudioSource audiosource in gameObject.GetComponents<AudioSource>()) {
            if ( audiosource.isPlaying && audiosource.clip != null ) {
                delay = (audiosource.clip.length - audiosource.time);
            }
        }

        Destroy(gameObject, delay);
    }
}
