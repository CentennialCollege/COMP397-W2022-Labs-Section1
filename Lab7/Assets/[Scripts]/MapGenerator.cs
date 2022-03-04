using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Public Properties exposed in the inspector
    [Header("Tile Resources")] 
    public List<GameObject> tilePrefabs;
    public GameObject startTile;
    public GameObject goalTile;

    [Header("Map Properties")] 
    [Range(2, 30)]
    public int width = 2;
    [Range(2, 30)]
    public int depth = 2;
    public Transform tileParent;

    [Header("Generated Tiles")] 
    public List<GameObject> tiles;

    // Private properties
    private int startWidth;
    private int startDepth;

    // Start is called before the first frame update
    void Start()
    {
        startWidth = width;
        startDepth = depth;
        BuildMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (width != startWidth || depth != startDepth)
        {
            ResetMap();
            BuildMap();
        }
    }

    public void ResetMap()
    {
        startWidth = width;
        startDepth = depth;
        var size = tiles.Count;

        // destroy every tile
        for (int i = 0; i < size; i++)
        {
            Destroy(tiles[i]);
        }

        tiles.Clear(); // remove all the tile references
    }

    public void BuildMap()
    {
        // place the start tile
        tiles.Add(Instantiate(startTile, Vector3.zero, Quaternion.identity, tileParent));

        // generate random tiles by width x depth
        for (int row = 0; row < depth; row++)
        {
            for (int col = 0; col < width; col++)
            {
                if (row == 0 && col == 0) { continue; }

                var randomTilePrefabIndex = Random.Range(0, 4);
                var randomTileRotation = Quaternion.Euler(0.0f, Random.Range(0, 4) * 90.0f, 0.0f);
                var randomTilePosition = new Vector3(col * 20.0f, 0.0f, row * 20.0f);
                var randomTile = Instantiate(tilePrefabs[randomTilePrefabIndex], randomTilePosition, randomTileRotation, tileParent);
                tiles.Add(randomTile);
            }
        }
    }
}
