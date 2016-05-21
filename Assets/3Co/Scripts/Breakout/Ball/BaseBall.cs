using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IBall {

}

public interface IGetMinSpeed {
    float Action(Rigidbody2D rigid, float nowMin, float baseMin);
}

public interface IGetMaxSpeed {
    float Action(Rigidbody2D rigid, float nowMax, float baseMax);
}

public abstract class BaseBall : MonoBehaviour, IBall {
    public List<IGetMinSpeed> GetMinSpeed = new List<IGetMinSpeed>();
    public List<IGetMaxSpeed> GetMaxSpeed = new List<IGetMaxSpeed>();

    public float baseMinSpeed = 5.0f;
    public float baseMaxSpeed = 25.0f;

    // when the ball needs to go from not moving to moving, do this
    public float forceOfRandomDirection = 300.0f;

    private float minSpeed;
    private float maxSpeed;

    // Use this for initialization
    public virtual void Awake() {
        minSpeed = baseMinSpeed;
        maxSpeed = baseMaxSpeed;
    }

    public virtual void Start() {
        GoInRandomDirection();
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

        foreach (IGetMinSpeed min in GetMinSpeed) {
            minSpeed = min.Action(rigid, minSpeed, baseMinSpeed);
        }

        foreach (IGetMaxSpeed max in GetMaxSpeed) {
            maxSpeed = max.Action(rigid, maxSpeed, baseMaxSpeed);
        }

        if (rigid.velocity.magnitude < minSpeed) {
            rigid.velocity = rigid.velocity.normalized * minSpeed;
        } else if (rigid.velocity.magnitude > maxSpeed) {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }

    public virtual void GoInRandomDirection(float howFast = 1.0f, bool doShout = false) {
        Vector3 v = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.forward) * Vector3.up;

        GetComponent<Rigidbody2D>().AddForce(v * forceOfRandomDirection);
    }
}
