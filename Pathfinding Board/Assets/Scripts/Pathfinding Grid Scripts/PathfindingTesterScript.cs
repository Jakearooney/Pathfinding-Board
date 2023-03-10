using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class PathfindingTesterScript : MonoBehaviour
{
    [SerializeField] public int setGridWidth;
    [SerializeField] public int setGridHeight;

    private int clickedXCoord = 0;
    private int clickedYCoord = 0;

    private PathfindingScript pathfinding;

    private void Start()
    {
        pathfinding = new PathfindingScript(setGridWidth, setGridHeight);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathnodeScript> path = pathfinding.FindPath(clickedXCoord, clickedYCoord, x, y);
            if (path != null)
            {
                for (int i=0; i<path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            clickedXCoord = x;
            clickedYCoord = y;
        }
    }


}
