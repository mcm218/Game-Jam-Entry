using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaveController : MonoBehaviour
    {
        /// <summary>
        /// THe wave's collider
        /// </summary>
        public Collider2D collider;

        /// <summary>
        /// Is the wave currently moving or is it stuck on a wall?
        /// </summary>
        public bool isMoving = true;

        /// <summary>
        /// Is the wave returning to the ocean or extending over the beach?
        /// </summary>
        public bool isReceding = false;

        public float waveDistance = 1f;

        [Range (0.1f, 10f)]
        public float waveSpeed = 0.5f;

        public float time = 0f;

        private float initialYPos;

        // Use this for initialization
        void Start()
        {
            // If needed, get the player's collider so we can detect if they touch anything
            if (this.collider == null) { this.collider = this.GetComponent<CircleCollider2D>(); }

            this.initialYPos = this.transform.localPosition.y;
            this.time = Mathf.PI;

            Debug.Log("Peak is " + (this.initialYPos - (this.waveDistance*2)));
        }

        public void Update()
        {
            this.MoveWave(this.time);
            this.time += Time.deltaTime;
        }

        public void MoveWave(float time)
        {
            float yPos = this.initialYPos - this.waveDistance - (this.waveDistance) * Mathf.Cos(time * this.waveSpeed);
            this.isReceding = (-this.waveDistance * this.waveSpeed * Mathf.Sin(time*this.waveSpeed)) < 0;

            // Wave Movement
            // If the wave is moving OR if the wave is receding
            if (this.isMoving || (this.isReceding && yPos >= this.transform.position.y))
            {
                this.isMoving = true;
                this.transform.position = new Vector2(0, yPos);
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Hit Object: " + collision.gameObject.name);
            // Did the wave hit a wall? If so, pause movement
            if (collision.gameObject.GetComponent<WallController>())
            {
                Debug.Log("HIT WALL");
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