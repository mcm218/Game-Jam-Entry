using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaveController : MonoBehaviour
    {

        public static WaveController Instance { get; set; }

        /// <summary>
        /// THe wave's collider
        /// </summary>
        public Collider2D collider;

        /// <summary>
        /// Is the wave returning to the ocean or extending over the beach?
        /// </summary>
        public bool isReceding = false;

        /// <summary>
        /// How far into the level does the wave travel
        /// </summary>
        public float waveDistance = 1f;

        /// <summary>
        /// How fast does the wave oscillate
        /// </summary>
        [Range (0.1f, 10f)]
        public float waveSpeed = 0.5f;

        /// <summary>
        /// Time value for wave oscillation
        /// </summary>
        public float time = 0f;

        /// <summary>
        /// Initial X position of the wave
        /// </summary>
        public float initialXPos;

        /// <summary>
        /// List of child wave segments
        /// </summary>
        public List<WaveChildController> waveChildren = new List<WaveChildController>();

        // Use this for initialization
        void Start()
        {
            WaveController.Instance = this;

            // If needed, get the player's collider so we can detect if they touch anything
            if (this.collider == null) { this.collider = this.GetComponent<CircleCollider2D>(); }

            // Set the initial x position
            this.initialXPos = this.transform.localPosition.x;

            // Set starting time to PI to shift the starting wave oscillation
            this.time = Mathf.PI;

            Debug.Log("Peak is " + (this.initialXPos - (this.waveDistance*2)));
        }

        public void Update()
        {
            this.MoveWaves(this.time);
            this.time += Time.deltaTime;
        }

        /// <summary>
        /// Attempts to move all the wave segments
        /// </summary>
        /// <param name="time"></param>
        public void MoveWaves(float time)
        {
            // Calculate the X pos of the wave
            float xPos = this.initialXPos - this.waveDistance - (this.waveDistance) * Mathf.Cos(time * this.waveSpeed);
             
            // Use the derivative to determine if the wave is receding or not
            this.isReceding = (-this.waveDistance * this.waveSpeed * Mathf.Sin(time*this.waveSpeed)) < 0;

            // Try to move each wave segment
            this.waveChildren.ForEach((wave) =>
            { 
                wave.MoveWave(xPos, isReceding); 
            });
        }

        public float getWaveXPos ()
        {
            return this.waveDistance - (this.waveDistance) * Mathf.Cos(time * this.waveSpeed);
        }
    }
}