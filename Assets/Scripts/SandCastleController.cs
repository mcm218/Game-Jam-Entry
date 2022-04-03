using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SandCastleController : MonoBehaviour
    {
        public static bool SandCastleDestroyed { get; set; } = false;

        public float healthPoints = 200;

        public float totalHealthPoints;

        public Image healthBarContainer;

        public Image healthBarFill;

        private void Start()
        {
            this.totalHealthPoints = this.healthPoints;
        }

        public bool TakeDamage(int damage)
        {
            this.healthPoints -= damage;
            // TODO: Change sprite for different damage threasholds

            return this.healthPoints <= 0;
        }

        public void LateUpdate()
        {
            this.healthBarFill.fillAmount = this.healthPoints / this.totalHealthPoints;

            // If the wall runs out of health, destroy it
            if (this.healthPoints <= 0)
            {
                SandCastleController.SandCastleDestroyed = true;

                RewardUIController.Instance.DisplayEnding();

                Destroy(this.healthBarContainer.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}