using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    private const float X_TILE_POS = 10f;
    private const float Y_TRIGGER_POS = 2.66f;
    private GameObject lastTile;
    [SerializeField] private GameObject generationTrigger;
    [SerializeField] private GameObject tilePrefab;

    // Start is called before the first frame update
    void Awake()
    {
        SetUpFirstTiles();
    }

    public void SetUpTiles()
    {
        GameObject lastLastTile = lastTile;
        float lastLastPos = lastLastTile.transform.position.z;
        for (int i = 1; i < 7; i++)
        {
            float z = lastLastTile.transform.position.z + 15*i;
            lastTile = Instantiate(tilePrefab, new Vector3(X_TILE_POS, 0f, z), Quaternion.identity);
            if (i == 2)
            {
                Instantiate(generationTrigger, new Vector3(0, Y_TRIGGER_POS, z), Quaternion.identity);
            }
        }
    }

    private void SetUpFirstTiles()
    {
        for (int i = 0; i < 6; i++)
        {
            lastTile = Instantiate(tilePrefab, new Vector3(X_TILE_POS, 0, (15*i) - 9), Quaternion.identity);
            if (i == 2)
            {
                Instantiate(generationTrigger, new Vector3(0, Y_TRIGGER_POS, (15 * i) - 1), Quaternion.identity);
            }
        }
    }
    
}