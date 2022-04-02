using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LitterController : MonoBehaviour
    {
        public int damage = 10;

        private Rigidbody2D rigidBody;

        private bool isStuck = false;

        // Use this for initialization
        void Start()
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (this.isStuck == false)
            {
                float xPos = WaveController.Instance.getWaveXPos();

                this.rigidBody.MovePosition(this.rigidBody.position + new Vector2(xPos - this.rigidBody.position.x, 0));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Did the litter hit a wall? If so, pause movement
            WallController wall = collision.gameObject.GetComponent<WallController>();
            if (wall)
            {
                Debug.Log("Hit object");

                // Damage the wall and if it's destroyed, don't stop the litter
                if (wall.TakeDamage(this.damage))
                {
                    return;
                }

                this.isStuck = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Did the litter exit a wall? If so, resume movement
            WallController wall = collision.gameObject.GetComponent<WallController>();
            if (wall)
            {
                Debug.Log("Hit object");

                this.isStuck = false;
            }

        }
    }
}