
using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class TableObject : UdonSharpBehaviour
{
    public int sizeX;
    public int sizeZ;

    public GameObject debugSphere;

    // Max numbers of tiles used will be 17, for now.
    private int tilesUsed = 0;
    // For now the limit of ships that can be placed will be 5.
    // This 2D array will represent the tiles of the table, and each tile will point to the gameobject that it is occupied by.
    // usedTiles[z][x] ==> [number][letter (in number form)]
    private GameObject[][] objectTiles = new GameObject[10][];
    private bool[][] damagedTiles = new bool[10][];

    private char[] dictionary = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };

    void Start()
    {
        for (int i = 0; i < 10; i++) 
        {
            objectTiles[i] = new GameObject[10];
            damagedTiles[i] = new bool[10];
            for (int j = 0; j < 10; j++) 
            {
                objectTiles[i][j] = null;
                damagedTiles[i][j] = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject ship = collision.gameObject;
        PlaceableObject shipPlaceable = ship.GetComponent<PlaceableObject>();
        if (shipPlaceable != null) 
        {
            BoxCollider shipCollider = ship.GetComponent<BoxCollider>();
            ship.transform.eulerAngles = shipPlaceable.fixRotation(ship.transform.eulerAngles);
            ship.transform.position = correctPosition(ship, shipCollider, shipPlaceable.getObjectVectors()[0]);
            useTiles(ship.transform, shipCollider);
            tilesUsed += (int) shipCollider.size.x;
        }
    }

    private Vector3 correctPosition(GameObject ship, BoxCollider objCollider, Vector3 fixVector) 
    {
        Vector3 testVertex = ship.transform.TransformPoint(fixVector);
        testVertex = gameObject.transform.InverseTransformPoint(testVertex);
        testVertex.x = Mathf.Round(testVertex.x);
        testVertex.z = Mathf.Round(testVertex.z);
        testVertex = gameObject.transform.TransformPoint(testVertex);
        Vector3 testCenter = ship.transform.InverseTransformPoint(testVertex);
        testCenter = new Vector3(testCenter.x - (objCollider.size.x / 2), testCenter.y, testCenter.z - (objCollider.size.z / 2));
        testCenter = ship.transform.TransformPoint(testCenter);
        return testCenter;
    }

    // It was this simple -w-
    private void useTiles(Transform shipTrans, BoxCollider shipCollider) 
    {
        float sideP = shipCollider.size.x / 2;
        float sideN = -(shipCollider.size.x / 2);
        // Adding +0.5 to the negative side will always result on the coordinates of the center of the first tile.
        sideN = sideN + 0.5f;
        // The center of each tile is 1m away from the next. To get the center of the next tile we just add 1.
        for (float i = sideN; i < sideP; i++) 
        {
            Vector3 center = shipTrans.TransformPoint(new Vector3(i, -0.5f, 0));
            center = transform.InverseTransformPoint(center);
            Debug.Log("Center of tile found at: " + center);
            Debug.Log("Tile translates to: " + (Mathf.Abs(truncate(center.z)) + 1) + "" + dictionary[(int)truncate(center.x)]);
            objectTiles[(int)Mathf.Abs(truncate(center.z))][(int)truncate(center.x)] = shipTrans.gameObject;
        }
    }

    private float truncate(float num) 
    {
        return (float) (int) num;
    }

    public bool isTileOccupied(int number, char letter) 
    {
        bool res = false;
        int letterInt = charToInt(letter);
        if (objectTiles[number-1][letterInt] != null)
        {
            res = true;
        }
        return res;
    }

    private int charToInt(char input) 
    {
        int res = 0;
        for (int i = 0; i < dictionary.Length; i++)
        {
            if (dictionary[i] == input)
            {
                res = i;
                break;
            }
        }
        return res;
    }

}
