using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEffector {

}

public abstract class BaseBall : MonoBehaviour, IBall {

    public List<Effect> powerups;

    public float baseMinSpeed = 5.0f;
    public float baseMaxSpeed = 25.0f;

    public AbstractPlayer creator;
    public GameObject owner;

    private float minSpeed;
    private float maxSpeed;

    // Use this for initialization
    public virtual void Awake () {
        minSpeed = baseMinSpeed;
        maxSpeed = baseMaxSpeed;
    }
	
	// Update is called once per frame
	public virtual void Update () {
        /*
        float _minSpeed = baseMinSpeed;
        foreach (IEffector effector in minSpeedEffectors) {

        }

        float _maxSpeed = baseMaxSpeed;
        */
    }

    public virtual void FixedUpdate() {
        ApplySpeedLimit();
    }

    public virtual void ApplySpeedLimit() {
        var rigid = GetComponent<Rigidbody2D>();

        if (rigid.velocity.magnitude < minSpeed) {
            rigid.velocity = rigid.velocity.normalized * minSpeed;
        } else if (rigid.velocity.magnitude > maxSpeed) {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }
}
