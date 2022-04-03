﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts
{
    public class RewardUIController : MonoBehaviour
    {
        public static RewardUIController Instance { get; set; }

        public Image image;

        public TextMeshProUGUI failureMessage;

        public Button restartButton;
        public Button closeButton;

        public Sprite goodEndingSprite;
        public Sprite badEndingSprite;

        public void Start()
        {
            RewardUIController.Instance = this;
            this.image.color = Color.clear;
            this.restartButton.transform.localScale  = Vector3.zero;
            this.failureMessage.transform.localScale = Vector3.zero;
            this.closeButton.transform.localScale    = Vector3.zero;
        }

        public void DisplayEnding ()
        {
            this.image.sprite = RoundController.Instance.earnedPicture ? this.goodEndingSprite : this.badEndingSprite;

            this.image.color = Color.white;

            // When showing destroyed sand castle, change the text
            // if they successfully captured a photo and add a button to 
            // close the polaroid to reveal the destroyed castle 'live'
            if (RoundController.Instance.earnedPicture)
            {
                this.closeButton.transform.localScale = Vector3.one;
                this.failureMessage.text = "We really need to find a better beach...";
            }
            else
            {
                // Otherwise, if the player failed, display the text for the destroyed
                // sand castle 'live' scene and show the 'restart' option
                this.restartButton.transform.localScale  = Vector3.one;
                this.failureMessage.transform.localScale = Vector3.one;
            }
        }

        // Method to close the success image and show the destroyed castel 
        public void closePolaroid ()
        {
            this.image.sprite = this.badEndingSprite;
            
            // Hide the clsoe button
            this.closeButton.transform.localScale    = Vector3.zero;

            // Show the restart button and failure message
            this.restartButton.transform.localScale  = Vector3.one;
            this.failureMessage.transform.localScale = Vector3.one;
        }
    }
}