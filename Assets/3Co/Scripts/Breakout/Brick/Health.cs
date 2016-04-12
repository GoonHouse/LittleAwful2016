using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SpriteState {
    public string name;
    public Sprite sprite;
    public Color color;
}

public class Health : MonoBehaviour {
    public UnityEvent onHurt;
    public UnityEvent onDeath;

    public float maxHealth = 3;
    public float health = 3;

    public List<SpriteState> hurtStates;

    public float damageDelay = 0.05f;

    private SpriteRenderer sr;

    public void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    public float TakeDamage(float amount = 1.0f) {
        health -= amount;

        if (health > 0.0f) {
            if (health > maxHealth) {
                health = maxHealth;
            }

            onHurt.Invoke();

            var h = Mathf.RoundToInt(health) - 1;
            sr.sprite = hurtStates[h].sprite;
            sr.color = hurtStates[h].color;
        }

        if (health <= 0.0f) {
            /*
            var pui = GetComponentInChildren<PowerUpItem>();

            if (pui != null) {
                pui.Detach();
            }
            */
            onDeath.Invoke();
        }

        return health;
    }

    void OnDestroy() {
        //onDeath.Invoke();
        //God.haggleLogic.numberOfBricks -= 1;
    }
}
