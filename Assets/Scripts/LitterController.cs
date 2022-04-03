using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class LitterController : MonoBehaviour
    {
        public int damage = 10;

        public Color damageColor = Color.red;

        private Rigidbody2D rigidBody;

        private bool isStuck = false;

        private SandCastleController castle;

        private WallController wall;

        public float timer = 0f;

        public float tickTime = 1f;

        // Use this for initialization
        void Start()
        {
            this.rigidBody = this.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (this.isStuck == false)
            {
                float xPos = WaveController.Instance.getWaveXPos();

                this.rigidBody.MovePosition(this.rigidBody.position + new Vector2(xPos - this.rigidBody.position.x, 0));
            }

            if (this.isStuck)
            {
                this.timer += Time.fixedDeltaTime;

                if (this.timer >= this.tickTime)
                {
                    this.timer = 0f;

                    // Is the litter stuck to wall?
                    if (this.wall)
                    {
                        // Tint the wall red to indicate it's taking damage
                        this.wall.GetComponent<SpriteRenderer>().color = this.damageColor;
                        // Damage the wall and if it's destroyed, don't stop the litter
                        if (this.wall.TakeDamage(this.damage))
                        {
                            this.isStuck = false;
                            return;
                        }
                    }

                    // Is the litter stuck to a castle?
                    if (this.castle)
                    {
                        // Tint the castle red to indicate it's taking damage
                        // TODO: NEED TO TINT ALL THE CASTLE SPRITES
                        this.castle.GetComponent<SpriteRenderer>().color = this.damageColor;
                        this.castle.TakeDamage(this.damage);
                    }

                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Did the litter hit a wall? If so, pause movement
            this.wall = collision.gameObject.GetComponent<WallController>();
            if (this.wall)
            {
                this.isStuck = true;
            }

            // Did the litter hit the castle?
            this.castle = collision.gameObject.GetComponent<SandCastleController>();
            if (this.castle)
            {
                this.isStuck = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Did the litter exit a wall? If so, resume movement
            WallController wall = collision.gameObject.GetComponent<WallController>();
            if (wall)
            {
                this.wall = null;
                this.isStuck = false;
            }

        }

        public void RemoveLitter ()
        {
            // Is the litter stuck to wall?
            if (this.wall)
            {
                // Clear any tint
                this.wall.GetComponent<SpriteRenderer>().color = Color.white;
            }

            // Is the litter stuck to a castle?
            if (this.castle)
            {
                // Clear any tint
                this.castle.GetComponent<SpriteRenderer>().color = Color.white;
            }

            Destroy(this.gameObject);
        }
    }
}