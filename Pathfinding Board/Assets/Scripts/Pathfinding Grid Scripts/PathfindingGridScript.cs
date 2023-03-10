using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

//No monobehaviour because it is a simple class.
public class PathfindingGridScript<GridObject>
{
    public const int heatMapMaxValue = 100;
    public const int heatMapMinValue = 0;

    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs
    {
        public int x;
        public int y;
    }


    public int width;
    public int height;
    public float cellSize;

    private Vector3 originPosition;
    
    //Defined Multidimensional arrays.
    private GridObject[,] gridArray;

    private TextMesh[,] debugTextArray;

    //Public constuctor which recieves the width and height from another script.
    public PathfindingGridScript(int width, int height, float cellSize, Vector3 originPosition, Func<PathfindingGridScript<GridObject>, int, int, GridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        this.originPosition = originPosition;

        gridArray = new GridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        debugTextArray = new TextMesh[width, height];

        //Tells the developer if the correct width and height are being used for the grid.
        Debug.Log(width + " " + height);

        //Cycles through the width of the grid (Example of cycling through the multi dimensional array established on line 14).
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            //Every time the width of the grid is cycled through creates the height below to create the rest of the grid.
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Tells the developer if the multidimensional, "gridArray", is being cycled through correctly.
                Debug.Log(x + " " + y);

                //Creates the world text for the grid using the "GetWorldPostition" function while cycling through the multidimensional array as well as colouring the text white and anchoring it to the middle center.
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);

                //Draws the horizontal and vertical grid lines but only in debug gizmo mode.
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        //Clears up the previously made grid on line 41/42 by joining up previously open boxes left.
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
            debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
        };
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
    public int GetCellSize()
    {
        return (int)cellSize;
    }

    //Function converts the x & y used into an actual world position.
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //Converts the world position into the grid position.
    public void GetXY(Vector3 worldPositon, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPositon - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPositon - originPosition).y / cellSize);
    }

    //Sets the value of one of the text numbers.
    public void SetGridObject(int x, int y, GridObject value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            //Mathf.Clamp clamps the value variable between the two values of heatmapmin and heatmapmax.
            gridArray[x, y] = value;

            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });

            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
        
    }

    //Checks the value of a grid cell, if it hasn't been changed yet changes it.
    public void SetGridObject(Vector3 worldPosition, GridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
        if (OnGridObjectChanged != null)
        {
            OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
        }
    }

    public void TriggerGridObjectChanged(int x, int y)
    {
        OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, y = y });
    }
    /*
    public void AddValue(int x, int y, int value)
    {
        SetValue(x, y, GetValue(x, y) + value);
    }
    */
    public GridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(GridObject);
        }
    }

    public GridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }

    /*
    public void AddValue(Vector3 worldPosition, int value, int fullValueRange, int totalRange)
    {
        int lowerValueAmount = Mathf.RoundToInt((float)value / (totalRange - fullValueRange));

        //Creates the heatmap on click.
        GetXY(worldPosition, out int originX, out int originY);
        for (int x = 0; x < totalRange; x++)
        {
            for (int y = 0; y < totalRange - x; y++)//Creates the diamond shaped heatmap.
            {
                int radius = x + y;
                int addValueAmount = value;
                if (radius > fullValueRange)
                {
                    addValueAmount -= lowerValueAmount * (radius - fullValueRange);
                }
                AddValue(originX + x, originY + y, addValueAmount);

                if (x != 0)
                {
                    AddValue(originX - x, originY + y, addValueAmount);
                }
                if (y != 0)
                {
                    AddValue(originX + x, originY - y, addValueAmount);
                    if (x != 0)
                    {
                        AddValue(originX - x, originY - y, addValueAmount);
                    }
                }

            }
        }
    }
    */
}
