using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class RoundController : MonoBehaviour
    {
        public static RoundController Instance { get; set; }
        public int currentRound = 0;

        public float timer = 0f;

        [Range(5f, 20f)]
        public float initialSetupTime = 10f;

        public bool setupComplete = false;

        [Range (10f, 30f)]
        public float roundCompleteTime = 20f;

        [Range (1, 20)]
        public int pictureRewardRound = 10;

        public bool earnedPicture = false;

        public FishController t1FishPrefab;
        public FishController t2FishPrefab;
        public FishController t3FishPrefab;

        public LitterController tirePrefab;
        public LitterController canPrefab;
        public LitterController plasticPrefab;

        public Transform topBound;

        public Transform bottomBound;

        [Range(1f, 3f)]
        public float difficultyModifier = 1.5f;

        public List<LitterController> newLitterList = new List<LitterController> ();

        public List<FishController> newFishList = new List<FishController> ();

        //public bool combatRoundActive = true;

        // Use this for initialization
        void Start()
        {
            RoundController.Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            this.timer += Time.deltaTime;

            if (this.setupComplete == false && this.timer >= this.initialSetupTime)
            {
                this.currentRound++;
                this.timer = 0f;
                this.setupComplete = true;
                this.StartNewRound(this.currentRound);
                return;
            }
            else if (this.timer >= this.roundCompleteTime)
            {
                this.currentRound++;
                this.timer = 0f;
                this.StartNewRound(this.currentRound);
                //this.StartCoroutine (this.StartNewRound(this.currentRound));
            }

            if (this.newLitterList.Count > 0)
            {
                if (WaveController.Instance.ReadyToMove ())
                {
                    this.newLitterList.ForEach(litter =>
                    {
                        litter.canStartMoving = true;
                    });
                    this.newLitterList.Clear();
                }
            }

            if (this.newFishList.Count > 0)
            {
                if (WaveController.Instance.ReadyToMove())
                {
                    this.newFishList.ForEach(fish =>
                    {
                        fish.canStartMoving = true;
                    });
                    this.newFishList.Clear();
                }
            }
        }

        private void StartNewRound (int roundNum)
        {
            this.earnedPicture = roundNum > this.pictureRewardRound;

            float timer = 0f;
            int totalSpawns = 2 + Mathf.FloorToInt(0.1f * Mathf.Pow(roundNum, this.difficultyModifier));

            // Double check that this is right... later...
            float timeBetweenSpawns = this.roundCompleteTime / totalSpawns;

            // Do stuff here later
            for (int index = 0; index < totalSpawns; index++)
            {
                // Get a random spawn position between the two bounds
                float randomY = Random.Range(this.bottomBound.position.y, this.topBound.position.y);
                float randomX = this.topBound.position.x;// Random.Range(this.topBound.position.x, this.bottomBound.position.x);

                float roll = Random.Range(0, 3);

                Vector2 randomPos = new Vector2(randomX, randomY);

                // If odd, spawn a fish
                if (index % 2 == 1) 
                {
                    FishController newFish;
                    if (roll < 1)
                    {
                        newFish = Instantiate<FishController>(this.t1FishPrefab);
                    }
                    else if (roll < 2)
                    {
                        newFish = Instantiate<FishController>(this.t2FishPrefab);
                    }
                    else
                    {
                        newFish = Instantiate<FishController>(this.t3FishPrefab);
                    }

                    newFish.transform.position = randomPos;
                    this.newFishList.Add(newFish);
                    Debug.Log("Spawning fish");
                }
                // If even, spawn litter
                else
                {
                    LitterController newLitter;
                    if (roll < 1)
                    {
                        newLitter = Instantiate<LitterController>(this.plasticPrefab);
                    }
                    else if (roll < 2)
                    {
                        newLitter = Instantiate<LitterController>(this.canPrefab);
                    }
                    else
                    {
                        newLitter = Instantiate<LitterController>(this.tirePrefab);
                    }

                    newLitter.transform.position = randomPos;
                    this.newLitterList.Add(newLitter);
                    Debug.Log("Spawning litter");
                }

                // Delay until its time for next spawn here
            }
        }
    }
}