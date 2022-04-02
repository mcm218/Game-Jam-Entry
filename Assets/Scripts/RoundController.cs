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

        public LitterController tirePrefab;

        public Transform topBound;

        public Transform bottomBound;

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
            // Do stuff here later
            for (int index = 0; index < roundNum * 2; index++)
            {
                // Get a random spawn position between the two bounds
                float randomY = Random.Range(this.bottomBound.position.y, this.topBound.position.y);
                Vector2 randomPos = new Vector2(topBound.position.x, randomY);

                // If even, spawn a fish
                if (index % 2 == 0) 
                {
                    FishController newFish = Instantiate<FishController>(this.t1FishPrefab);
                    newFish.transform.position = randomPos;
                    Debug.Log("Spawning fish");
                }
                // If odd, spawn litter
                else
                {
                    LitterController newLitter = Instantiate<LitterController>(this.tirePrefab);
                    newLitter.transform.position = randomPos;
                    Debug.Log("Spawning litter");
                }
            }
        }
    }
}