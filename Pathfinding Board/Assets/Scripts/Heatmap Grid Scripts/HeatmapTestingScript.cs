using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class HeatmapTestingScript : MonoBehaviour
{
    public int setGridWidth;
    public int setGridHeight;
    public float setGridCellSize;

    [SerializeField] private HeatmapVisualScript heatMapVisual;

    private HeatmapGridScript grid;

    private void Start()
    {
        //Feeds the two public variables into the HeatmapGridScript script to generate a grid with the player set width and height through the scene. Also sets the grid to the center of the scene at the end.
        grid = new HeatmapGridScript(setGridWidth, setGridHeight, setGridCellSize, new Vector3(0, 0));

        heatMapVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();
            grid.AddValue(position, 100, 5, 20);
        }
    }

    
}
