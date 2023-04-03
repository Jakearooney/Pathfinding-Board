using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class GenericsTestingScript : MonoBehaviour
{
    public int setGridWidth;
    public int setGridHeight;
    public float setGridCellSize;

    [SerializeField] private GenericsHeatmapVisualScript heatMapVisual;

    [SerializeField] private GenericsHeatmapBoolVisualScript heatMapBoolVisual;

    [SerializeField] private GenericsHeatmapGenericsVisualScript heatMapGenericsVisual;

    private GenericsGridScript<HeatMapGridObject> grid;

    

    private void Start()
    {
        //Feeds the two public variables into the GenericsGridScript script to generate a grid with the player set width and height through the scene. Also sets the grid to the center of the scene at the end.
        grid = new GenericsGridScript<HeatMapGridObject>(setGridWidth, setGridHeight, setGridCellSize, new Vector3(0, 0), (GenericsGridScript<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        heatMapGenericsVisual.SetGrid(grid);

        //heatMapVisual.SetGrid(grid);
        //heatMapBoolVisual.SetGrid(grid);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 position = UtilsClass.GetMouseWorldPosition();

            HeatMapGridObject heatMapGridObject = grid.GetGridObject(position);

            if (heatMapGridObject != null)
            {
                heatMapGridObject.addValue(5);
            }

            //grid.AddValue(position, 100, 2, 25);
            //grid.SetValue(position, true);
        }
    }
}

public class HeatMapGridObject
{
    private const int min = 0;
    private const int max = 100;

    private GenericsGridScript<HeatMapGridObject> grid;

    private int x;
    private int y;
    private int value;

    public HeatMapGridObject(GenericsGridScript<HeatMapGridObject> grid, int x, int y)
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


