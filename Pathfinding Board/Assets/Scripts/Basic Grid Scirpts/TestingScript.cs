using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TestingScript : MonoBehaviour
{
    public int setGridWidth;
    public int setGridHeight;
    public float setGridCellSize;

    private GridScript grid;

    private void Start()
    {
        //Feeds the two public variables into the gridscript script to generate a grid with the player set width and height through the scene. Also sets the grid to the center of the scene at the end.
        grid = new GridScript(setGridWidth, setGridHeight, setGridCellSize, new Vector3((setGridWidth / 2) * -setGridCellSize + (-setGridCellSize / 2), (setGridHeight / 2) * -setGridCellSize));
    }

    private void Update()
    {
        //Checks if the player has clicked a grid cell, if so changes the value.
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }

        //Checks if the player right clicked a grid cell, if so reads its value and tells the developer.
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
