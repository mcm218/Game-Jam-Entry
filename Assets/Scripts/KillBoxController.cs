using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class KillBoxController : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}