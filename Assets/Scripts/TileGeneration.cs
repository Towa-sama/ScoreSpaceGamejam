using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    private const float X_TILE_POS = 10f;
    private const float Y_TRIGGER_POS = 2.66f;
    private GameObject lastTile;
    [SerializeField] private GameObject generationTrigger;
    [SerializeField] private GameObject tilePrefab;
    private List<GameObject> oldTiles = new();

    // Start is called before the first frame update
    void Awake()
    {
        SetUpFirstTiles();
    }

    private void SetUpTiles()
    {
        for (int z = (int) lastTile.transform.position.z; z <= (int) lastTile.transform.position.z + 6*15; z+=15)
        {
            lastTile = Instantiate(tilePrefab, new Vector3(X_TILE_POS, 0, z), Quaternion.identity);
        }
    }

    private void DeleteOldTiles()
    {
        foreach (var oldTile in oldTiles)
        {
            Destroy(oldTile);
        }
    }

    private void SetUpFirstTiles()
    {
        for (int i = 0; i < 6; i++)
        {
            lastTile = Instantiate(tilePrefab, new Vector3(X_TILE_POS, 0, (15*i) - 9), Quaternion.identity);
            oldTiles.Add(lastTile);
            if (i == 2)
            {
                Instantiate(generationTrigger, new Vector3(0, Y_TRIGGER_POS, (15 * i) - 1), Quaternion.identity);
            }
        }
    }
    
}