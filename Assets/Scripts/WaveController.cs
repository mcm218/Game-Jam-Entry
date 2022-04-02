using System.Collections;
using System.Collections.Generic;
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
        /// Is the wave returning to the ocean or extending over the beach?
        /// </summary>
        public bool isReceding = false;

        public float waveDistance = 1f;

        [Range (0.1f, 10f)]
        public float waveSpeed = 0.5f;

        public float time = 0f;

        private float initialXPos;

        public List<WaveChildController> waveChildren = new List<WaveChildController>();

        // Use this for initialization
        void Start()
        {
            // If needed, get the player's collider so we can detect if they touch anything
            if (this.collider == null) { this.collider = this.GetComponent<CircleCollider2D>(); }

            this.initialXPos = this.transform.localPosition.x;
            this.time = Mathf.PI;

            Debug.Log("Peak is " + (this.initialXPos - (this.waveDistance*2)));
        }

        public void Update()
        {
            this.MoveWaves(this.time);
            this.time += Time.deltaTime;
        }

        public void MoveWaves(float time)
        {
            float xPos = this.initialXPos - this.waveDistance - (this.waveDistance) * Mathf.Cos(time * this.waveSpeed);
            this.isReceding = (-this.waveDistance * this.waveSpeed * Mathf.Sin(time*this.waveSpeed)) < 0;

            // Try to move each wave segment
            this.waveChildren.ForEach((wave) =>
            { 
                wave.MoveWave(xPos, isReceding); 
            });
        }
    }
}