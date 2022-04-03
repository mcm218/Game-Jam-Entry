using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaveController : MonoBehaviour
    {
        /// <summary>
        /// Gets or sets the main WaveController Instance
        /// </summary>
        public static WaveController Instance { get; set; }

        /// <summary>
        /// Is the wave returning to the ocean or extending over the beach?
        /// </summary>
        public bool isReceding = false;

        /// <summary>
        /// How far into the level does the wave travel
        /// </summary>
        public float waveDistance = 1f;

        /// <summary>
        /// How far into the level does the wave travel during the setup phase
        /// </summary>
        public float setupWaveDistance = 5f;

        private float currentWaveDistance;

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
            // Set the WaveController Instance
            WaveController.Instance = this;

            this.currentWaveDistance = this.setupWaveDistance;

            // Set the initial x position
            this.initialXPos = this.transform.position.x;

            // Set starting time to PI to shift the starting wave oscillation
            this.time = Mathf.PI / this.waveSpeed;

            Debug.Log("Peak is " + (this.initialXPos - (this.waveDistance*2)));
            Debug.Log("Trough is" + this.initialXPos);
        }

        public void Update()
        {
            // Every frame, move the waves
            this.MoveWaves(this.time);

            // Update the time
            this.time += Time.deltaTime;

            if (this.waveDistance != this.currentWaveDistance && RoundController.Instance.setupComplete) { this.TryToUpdateDistance (this.waveDistance); }
        }

        private void TryToUpdateDistance(float newWaveDistance)
        {
            // Calculate the X pos of the wave
            float xPos = this.initialXPos - this.currentWaveDistance - (this.currentWaveDistance) * Mathf.Cos(time * this.waveSpeed);
            // Is the wave currently within 0.1 units of its initial x position? If so, update the wave distance
            if (Mathf.Abs (this.initialXPos - xPos) <= 0.1) { this.currentWaveDistance = newWaveDistance; }
        }

        public bool ReadyToMove ()
        {
            // Calculate the X pos of the wave
            float xPos = this.initialXPos - this.currentWaveDistance - (this.currentWaveDistance) * Mathf.Cos(time * this.waveSpeed);
            // Is the wave currently within 0.1 units of its initial x position? If so, return true
            return Mathf.Abs(this.initialXPos - xPos) <= 0.1;
        }

        /// <summary>
        /// Attempts to move all the wave segments
        /// </summary>
        /// <param name="time"></param>
        public void MoveWaves(float time)
        {
            // Calculate the X pos of the wave
            float xPos = this.initialXPos - this.currentWaveDistance - (this.currentWaveDistance) * Mathf.Cos(time * this.waveSpeed);
             
            // Use the derivative to determine if the wave is receding or not
            this.isReceding = (-this.waveDistance * this.waveSpeed * Mathf.Sin(time*this.waveSpeed)) < 0;

            // Try to move each wave segment
            this.waveChildren.ForEach((wave) => { wave.MoveWave(xPos, isReceding); });
        }

        /// <summary>
        /// Gets the wave's current x position
        /// </summary>
        public float getWaveXPos ()
        {
            // Add 3 to shift items a little bit back
            // The sprite pivots extend past the visible part of the wave, so this is just a dirty fix
            return this.initialXPos - this.currentWaveDistance - (this.currentWaveDistance) * Mathf.Cos(this.time * this.waveSpeed) + 3;
        }
    }
}