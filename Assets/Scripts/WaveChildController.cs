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

        /// <summary>
        /// The collider for this wave segment
        /// </summary>
        public Collider2D waveCollider;

        /// <summary>
        /// The sprite renderer for this wave segment
        /// </summary>
        public SpriteRenderer spriteRenderer;

        /// <summary>
        /// Attempts to move this wave segments
        /// </summary>
        /// <param name="xPos">The X Position to move towards</param>
        /// <param name="isReceding">Whether or not the parent wave is receding</param>
        public void MoveWave(float xPos, bool isReceding)
        {
            // Wave Movement
            // If the wave is moving OR if the wave is receding, update its position
            if (this.isMoving || (isReceding && xPos >= this.transform.position.x))
            {
                // Reset isMoving to true so the wave segment doesn't get stuck
                this.isMoving = true;

                // Update the wave segments position
                this.transform.position = new Vector2(xPos, this.transform.position.y);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Did the wave hit a wall? If so, pause movement
            WallController wall = collision.gameObject.GetComponent<WallController>();
            if (wall)
            {
                // Damage the wall and if it's destroyed, don't stop the wave segment
                if (wall.TakeDamage(this.damage))
                {
                    return;
                }

                this.isMoving = false;
            }

            // Did the wave hit the castle? If so, pause movement
            SandCastleController castle = collision.gameObject.GetComponent<SandCastleController>();
            if (castle)
            {
                // Damage the wall and if it's destroyed, don't stop the wave segment
                if (castle.TakeDamage(this.damage))
                {
                    return;
                }

                this.isMoving = false;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Is the wave no longer hitting a wall? If so, allow the wave to keep moving
            if (collision.gameObject.GetComponent<WallController>())
            {
                this.isMoving = true;
            }
        }
    }
}