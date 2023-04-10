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

    [SerializeField] private LineRenderer lineRendererPrefab;
    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

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
                if (lineRenderers.Count >= 2)
                {
                    Destroy(lineRenderers[0].gameObject);
                    lineRenderers.RemoveAt(0);
                }

                LineRenderer lineRenderer = Instantiate(lineRendererPrefab);
                lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                lineRenderer.startColor = Color.black;
                lineRenderer.endColor = Color.black;
                lineRenderer.startWidth = 0.5f;
                lineRenderer.endWidth = 0.5f;

                Vector3[] positions = new Vector3[path.Count + 1];
                for (int i = 0; i < path.Count; i++)
                {
                    positions[i] = new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f;
                }
                positions[path.Count] = mouseWorldPosition;

                lineRenderer.positionCount = positions.Length;
                lineRenderer.SetPositions(positions);

                lineRenderers.Add(lineRenderer);

                clickedXCoord = x;
                clickedYCoord = y;
            }
        }
    }


}
