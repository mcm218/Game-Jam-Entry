using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class GridController : MonoBehaviour
    {
        public Grid grid;

        public Tilemap tilemap;

        // Use this for initialization
        void Start()
        {
            if (this.grid == null) { this.grid = this.GetComponent<Grid>(); }
            if (this.tilemap == null) { this.tilemap = this.GetComponent<Tilemap>(); }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown (0))
            {
                // Get the position of the mouse in the screen
                Vector3 mousePos = Input.mousePosition;

                // Convert the mouse position to a point in the world
                Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                Debug.Log(this.grid.WorldToCell(worldPos));
            }
        }
    }
}