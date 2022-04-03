using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class RoundController : MonoBehaviour
    {
        public int currentRound = 1;

        public float timer = 0f;

        public float roundCompleteTime = 20f;

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

        //public bool combatRoundActive = true;

        // Use this for initialization
        void Start()
        {
            this.StartNewRound(this.currentRound);
        }

        // Update is called once per frame
        void Update()
        {
            this.timer += Time.deltaTime;

            if (this.timer >= this.roundCompleteTime)
            {
                this.currentRound++;
                this.timer = 0f;
                this.StartNewRound(this.currentRound);
            }
        }

        private void StartNewRound (int roundNum)
        {
            int totalSpawns = 1 + Mathf.FloorToInt(0.1f * Mathf.Pow(roundNum, this.difficultyModifier));

            // Do stuff here later
            for (int index = 0; index < totalSpawns; index++)
            {
                // Get a random spawn position between the two bounds
                float randomY = Random.Range(this.bottomBound.position.y, this.topBound.position.y);
                float randomX = Random.Range(this.topBound.position.x, this.bottomBound.position.x);

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
                    Debug.Log("Spawning litter");
                }
            }
        }
    }
}