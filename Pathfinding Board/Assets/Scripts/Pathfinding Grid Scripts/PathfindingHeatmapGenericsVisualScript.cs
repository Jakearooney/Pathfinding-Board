using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathfindingHeatmapGenericsVisualScript : MonoBehaviour
{
    private PathfindingGridScript<PathfindingHeatMapGridObject> grid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake()
    {
        //Creating the mesh for the heatmap.
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(PathfindingGridScript<PathfindingHeatMapGridObject> grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();

        grid.OnGridObjectChanged += PathfindingGridScript_OnGridObjectChanged;
    }

    private void PathfindingGridScript_OnGridObjectChanged(object sender, PathfindingGridScript<PathfindingHeatMapGridObject>.OnGridObjectChangedEventArgs e)
    {
        //UpdateHeatMapVisual();
        updateMesh = true;
    }

    private void LateUpdate()
    {
        if (updateMesh)
        {
            updateMesh = false;
            UpdateHeatMapVisual();
        }    
    }



    private void UpdateHeatMapVisual()
    {
        //Creating the empyt mesh array then cycling through each grid position to update it correctly.
        PathfindingMeshUtilsScript.CreateEmptyMeshArrays(grid.GetWidth() * grid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                PathfindingHeatMapGridObject gridObject = grid.GetGridObject(x, y);
                float gridValueNormalized = gridObject.GetValueNormalized();
                Vector2 gridValueUV = new Vector2(gridValueNormalized, 0f);

                PathfindingMeshUtilsScript.AddToMeshArrays(vertices, uv, triangles, index, grid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, gridValueUV, gridValueUV);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }
}
