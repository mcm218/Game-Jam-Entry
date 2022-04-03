using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class SandCastleController : MonoBehaviour
    {
        public int healthPoints = 200;

        public bool TakeDamage(int damage)
        {
            this.healthPoints -= damage;
            // TODO: Change sprite for different damage threasholds

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
}