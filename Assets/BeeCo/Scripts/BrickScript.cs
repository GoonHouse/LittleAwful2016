using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {
    public int Health = 3;
    float countDown = .05f;
    public void TakeDamage()
    {
        Health--;
    }

    void Update()
    {
        if (Health<=0)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0) Destroy(gameObject);
        }
    }
}
