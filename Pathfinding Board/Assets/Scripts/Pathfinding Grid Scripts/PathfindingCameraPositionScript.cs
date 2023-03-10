using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingCameraPositionScript : MonoBehaviour
{
    [SerializeField] private PathfindingTesterScript pathfindTest;

    [SerializeField] private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.position = new Vector3(pathfindTest.setGridWidth * 10 / 2, pathfindTest.setGridHeight * 10 / 2, -50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
