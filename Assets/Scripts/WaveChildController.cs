using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaveChildController : MonoBehaviour
    {
        public int damage = 25;

        /// <summary>
        /// Is the wave currently moving or is it stuck on a wall?
        /// </summary>
        public bool isMoving = true;

        public Collider2D waveCollider;

        public SpriteRenderer spriteRenderer;

        public void MoveWave(float xPos, bool isReceding)
        {
            // Wave Movement
            // If the wave is moving OR if the wave is receding, update its position
            if (this.isMoving || (isReceding && xPos >= this.transform.position.x))
            {
                this.isMoving = true;
                this.transform.position = new Vector2(xPos, this.transform.position.y);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Hit Object: " + collision.gameObject.name);
            WallController wall = collision.gameObject.GetComponent<WallController>();
            // Did the wave hit a wall? If so, pause movement
            if (wall)
            {
                Debug.Log("HIT WALL");

                if (wall.TakeDamage(this.damage))
                {
                    Debug.Log("WALL DESTROYED");
                    return;
                }

                this.isMoving = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Debug.Log("Left Object: " + collision.gameObject.name);
            // Is the wave no longer hitting a wall?
            if (collision.gameObject.GetComponent<WallController>())
            {
                Debug.Log("LEFT WALL");
                this.isMoving = true;
            }
        }
    }
}