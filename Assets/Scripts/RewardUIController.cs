﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RewardUIController : MonoBehaviour
    {
        public static RewardUIController Instance { get; set; }

        public Image image;

        public Button restartButton;

        public Sprite goodEndingSprite;

        public Sprite badEndingSprite;

        public void Start()
        {
            RewardUIController.Instance = this;
            this.image.color = Color.clear;
            this.restartButton.transform.localScale = Vector3.zero;
        }

        public void DisplayEnding ()
        {
            this.image.sprite = RoundController.Instance.earnedPicture ? this.goodEndingSprite : this.badEndingSprite;

            this.image.color = Color.white;
            this.restartButton.transform.localScale = Vector3.one;
        }
    }
}