using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

//No monobehaviour because it is a simple class.
public class GridScript 
{
    private int width;
    private int height;
    private float cellSize;

    private Vector3 originPosition;
    
    //Defined Multidimensional arrays.
    private int[,] gridArray;

    private TextMesh[,] debugTextArray;

    //Public constuctor which recieves the width and height from another script.
    public GridScript(int width, int height, float cellSize, Vector3 originPosition) 
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        this.originPosition = originPosition;

        gridArray = new int[width, height];
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
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);

                //Draws the horizontal and vertical grid lines but only in debug gizmo mode.
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        //Clears up the previously made grid on line 41/42 by joining up previously open boxes left.
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    //Function converts the x & y used into an actual world position.
    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize + originPosition;
    }

    //Converts the world position into the grid position.
    private void GetXY(Vector3 worldPositon, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPositon - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPositon - originPosition).y / cellSize);
    }

    //Sets the value of one of the text numbers.
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
        
    }

    //Checks the value of a grid cell, if it hasn't been changed yet changes it.
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}
