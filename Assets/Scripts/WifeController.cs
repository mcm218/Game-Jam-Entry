using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class WifeController : MonoBehaviour
    {
        public Transform startPosition;

        public Transform endPosition;

        private float speed = 75f;

        public static WifeController Instance { get; set; }

        public bool isMoving = false;

        private Rigidbody2D rigidBody;

        private void Start()
        {
            WifeController.Instance = this;

            this.rigidBody = this.GetComponent<Rigidbody2D>();

            this.transform.position = this.startPosition.position;
        }
        public void StartMoving ()
        {
            this.isMoving = true;
            Vector3 direction = this.endPosition.position - this.transform.position;
            this.rigidBody.AddRelativeForce(direction.normalized * speed);
        }
    }
}