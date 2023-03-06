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

    [SerializeField] private HeatmapVisualScript heatmapVisual;
    [SerializeField] private GenericsVisualScript heatMapboolVisual;
    [SerializeField] private GenericsVisualGenericsScript heatMapGenericVisual;

    private GenericsGridScript<HeatMapGridObject> grid;

    private void Start()
    {
        //Feeds the two public variables into the HeatmapGridScript script to generate a grid with the player set width and height through the scene. Also sets the grid to the center of the scene at the end.
        grid = new GenericsGridScript<HeatMapGridObject>(setGridWidth, setGridHeight, setGridCellSize, new Vector3(-setGridWidth * setGridCellSize / 2, -setGridHeight * setGridCellSize / 2), (GenericsGridScript<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
        //heatMapVisual.SetGrid(grid);
        //heatMapboolVisual.SetGrid(grid);
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

           // grid.SetValue(position, true);
        }
    }

    public class HeatMapGridObject
    {
        private const int min = 0;
        private const int max = 0;

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
            Mathf.Clamp(value, min, max);
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
}
