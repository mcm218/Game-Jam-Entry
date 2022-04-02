using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts
{
    public class GridController : MonoBehaviour
    {
        public static GridController Instance { get; private set; }
        public Grid grid;

        public Tilemap tilemap;

        // Use this for initialization
        void Start()
        {
            GridController.Instance = this;

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

                
            }
        }

        public Vector2 GetTileCenter (Vector2 worldPos)
        {
            Vector3Int gridCoords = this.grid.WorldToCell(worldPos);

            return this.grid.GetCellCenterWorld (gridCoords);
        }
    }
}