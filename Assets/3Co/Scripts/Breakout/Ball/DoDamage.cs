using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoDamage : MonoBehaviour {
    public List<string> filteredTags;
    public float damageAmount;
    public bool isScaledByForce;

    public virtual float Hurt(GameObject target, float scale = 1.0f) {
        var th = target.GetComponent<Health>();
        if( th != null ) {
            return th.TakeDamage(damageAmount * scale);
        }
        return 0.0f;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if( filteredTags.Contains(coll.gameObject.tag) ) {
            var scale = GetCollisionForce(coll.rigidbody);
            Hurt(coll.gameObject, scale);
        }
    }

    public virtual float GetCollisionForce(Rigidbody2D rigid) {
        var scale = 1.0f;
        if (isScaledByForce) {
            scale = God.KineticEnergy(rigid);
        }
        return scale;
    }
}
