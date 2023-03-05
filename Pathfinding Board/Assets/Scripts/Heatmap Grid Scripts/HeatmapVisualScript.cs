using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatmapVisualScript : MonoBehaviour
{
    private HeatmapGridScript grid;

    [SerializeField] private MeshUtilsScript meshUtils;

    private Mesh mesh;

    private void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(HeatmapGridScript grid)
    {
        this.grid = grid;
        UpdateHeatMapVisual();
    }

    

    private void UpdateHeatMapVisual()
    {
        meshUtils.CreateEmptyMeshData(grid.GetWidth() * grid.GetHeight(), out Vector3[] verticies, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                int index = x * grid.GetHeight() + y;
                Debug.Log(index);
            }
        }
    }
}
