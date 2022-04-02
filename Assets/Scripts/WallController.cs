using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public int healthPoints = 50;


    public bool TakeDamage (int damage)
    {
        this.healthPoints -= damage;
        // TODO: Change sprite if below threshold

        return this.healthPoints <= 0;
    }

    public void LateUpdate()
    {

        // If the wall runs out of health, destroy it
        if (this.healthPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
