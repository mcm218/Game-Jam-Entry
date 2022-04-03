using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{
    public int damage = 10;

    private Rigidbody2D rigidBody;

    public bool canStartMoving = false;

    // Use this for initialization
    void Start()
    {
        this.rigidBody = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (this.canStartMoving == false) { return; }

        float xPos = WaveController.Instance.getWaveXPos();

        this.rigidBody.MovePosition(this.rigidBody.position + new Vector2(xPos - this.rigidBody.position.x, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Did the fish hit a wall? 
        WallController wall = collision.gameObject.GetComponent<WallController>();
        if (wall)
        {
            wall.TakeDamage(this.damage);

            // TODO: Shrink the fish's scale and then destroy it
            Destroy(this.gameObject);
        }

        // Did the fish hit the castle?
        SandCastleController castle = collision.gameObject.GetComponent<SandCastleController>();
        if (castle)
        {
            castle.TakeDamage(this.damage);

            // TODO: Shrink the fish's scale and then destroy it
            Destroy(this.gameObject);
        }
    }
}
