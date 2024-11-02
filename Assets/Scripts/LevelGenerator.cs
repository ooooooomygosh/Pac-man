using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject outsideCorner; // 1
    public GameObject outsideWall; // 2
    public GameObject insideCorner; // 3
    public GameObject insideWall; // 4
    public GameObject standardPallet; // 5
    public GameObject powerPallet; // 6
    public GameObject tJunction; // 7

    public GameObject levelParent;

    // 14 columns * 15 rows = 210 tiles
    int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7}, // 0
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4}, // 1
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4}, // 2
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4}, // 3
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3}, // 4
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5}, // 5
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4}, // 6
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3}, // 7
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4}, // 8
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4}, // 9
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3}, // 10 
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0}, // 11
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0}, // 12
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0}, // 13
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, // 14
    };

    // Start is called before the first frame update
    void Start()
    {
        for (int row = 0; row < 15; row++) 
        { 
            for (int col = 0; col < 14; col++)
            {
                int tileNum = levelMap[row, col]; // store current tile number into a variable 
                if (tileNum != 0) // checks if its not 0 (empty tile)
                {
                    GameObject tile = GameObject.FindWithTag(tileNum + "");
                    GameObject rotateTile = GameObject.Instantiate(tile, new Vector2(col, -row), Quaternion.identity);

                    rotateTile.transform.parent = levelParent.transform;
                    //rotateTile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    int rotNum = 0; // store z value for rotating

                    // Rotating outside corners
                    if (tileNum == 1)
                    {
                        if (row == 9 && col == 0) { rotNum += 90; }
                        if (row == 9 && col == 5) { rotNum -= 90; }
                        if (row == 13) { rotNum += 180; }
                        rotateTile.transform.Rotate(0f, 0f, rotNum);
                    }

                    // Rotating outside walls
                    if (tileNum == 2) { 
                        if(row >= 1 && row <= 8 || row >= 10 && row <= 12)
                            rotateTile.transform.Rotate(0f, 0f, 90f);
                    }

                    // Rotating inside corners
                    if (tileNum == 3)
                    {
                        // Rotate inside corners - top-right 
                        if (row == 2 && (col == 5 || col == 11)) { rotNum = 270; }
                        if (row == 6 && (col == 5 || col == 8)) { rotNum = 270; }
                        if (row == 7 && col == 13) { rotNum = 270; }
                        if (row == 9 && col == 11) { rotNum = 270; }

                        // Rotate inside corners - bottom-left
                        if ((row == 4 || row == 10) && (col == 2 || col == 7 || col == 13)) { rotNum = 90; }
                        if ((row == 7 || row == 9) && (col == 2 || col == 7 || col == 8 || col == 10)) { rotNum = 90; }
                        if (row == 13 && col == 7) { rotNum = 90; }
                        
                        // Rotate inside corners - bottom-right
                        if ((row == 4 || row == 7 || row == 10) && (col == 5 || col == 11)) { rotNum = 180; }
                        if ( row == 13 && col == 8) { rotNum = 180; }

                        rotateTile.transform.Rotate(0f, 0f, rotNum);
                    }

                    // Rotating inside walls
                    if (tileNum == 4)
                    {
                        if (col == 13 && (row == 2 || row == 9))
                            rotateTile.transform.Rotate(0f, 0f, 90f);
                        if (row == 1 || row == 3 || row == 8 || row == 11 || row == 13 || row == 14 && col != 13)
                        {
                            rotateTile.transform.Rotate(0f, 0f, 90f);
                        }    
                        if ((row == 7 || row == 9 || row == 10 || row == 12) && (col == 7 || col == 8))
                        {
                            rotateTile.transform.Rotate(0f, 0f, 90f);
                        }     
                    }
                    //Debug.Log("Instantiated: " + tile);
                }
                //Debug.Log("Row: " + row + " || Column: " + col + " || Tile Number: " + levelMap[row, col]);
            }
        }

        // Deactive all LevelSprites game objects in LevelManager after instantiating 
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        
        //Debug.Log(gameObject.transform.childCount);

        // Instantiate the rest of the grids. Place within the LevelManager as a child. 
        GameObject.Instantiate(gameObject.transform.GetChild(1), new Vector2(27, 0), Quaternion.Euler(0, 180, 0), gameObject.transform.GetChild(1).parent);
        GameObject.Instantiate(gameObject.transform.GetChild(1), new Vector2(27, -28), Quaternion.Euler(-180, 180, 0), gameObject.transform.GetChild(1).parent);
        GameObject.Instantiate(gameObject.transform.GetChild(1), new Vector2(0, -28), Quaternion.Euler(-180, 0, 0), gameObject.transform.GetChild(1).parent);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
