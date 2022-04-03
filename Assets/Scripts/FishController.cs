using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public int damage = 10;

    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float xPos = WaveController.Instance.getWaveXPos();

        this.rigidBody.MovePosition(this.rigidBody.position + new Vector2(xPos - this.rigidBody.position.x, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Did the litter hit a wall? If so, pause movement
        WallController wall = collision.gameObject.GetComponent<WallController>();
        if (wall)
        {
            Debug.Log("Hit object");

            wall.TakeDamage(this.damage);

            // TODO: Shrink the fish's scale and then destroy it
            Destroy(this.gameObject);
        }
    }
}
