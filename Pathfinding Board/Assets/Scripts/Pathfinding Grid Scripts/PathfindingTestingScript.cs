using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class PathfindingTestingScript : MonoBehaviour
{
    public int setGridWidth;
    public int setGridHeight;
    public float setGridCellSize;

    [SerializeField] private PathfindingHeatmapVisualScript heatMapVisual;

    [SerializeField] private PathfindingHeatmapBoolVisualScript heatMapBoolVisual;

    [SerializeField] private PathfindingHeatmapGenericsVisualScript heatMapGenericsVisual;

    private PathfindingGridScript<PathfindingHeatMapGridObject> grid;

    

    private void Start()
    {
        //Feeds the two public variables into the GenericsGridScript script to generate a grid with the player set width and height through the scene. Also sets the grid to the center of the scene at the end.
        grid = new PathfindingGridScript<PathfindingHeatMapGridObject>(setGridWidth, setGridHeight, setGridCellSize, new Vector3(-setGridWidth * setGridCellSize / 2, -setGridHeight * setGridCellSize / 2), (PathfindingGridScript<PathfindingHeatMapGridObject> g, int x, int y) => new PathfindingHeatMapGridObject(g, x, y));
        heatMapGenericsVisual.SetGrid(grid);

        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();

            PathfindingHeatMapGridObject pathfindingHeatMapGridObject = grid.GetGridObject(position);

            if (pathfindingHeatMapGridObject != null)
            {
                pathfindingHeatMapGridObject.addValue(5);
            }

            //grid.AddValue(position, 100, 2, 25);
            //grid.SetValue(position, true);
        }
    }
}

public class PathfindingHeatMapGridObject
{
    private const int min = 0;
    private const int max = 100;

    private PathfindingGridScript<PathfindingHeatMapGridObject> grid;

    private int x;
    private int y;
    private int value;

    public PathfindingHeatMapGridObject(PathfindingGridScript<PathfindingHeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void addValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, min, max);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / max;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}


