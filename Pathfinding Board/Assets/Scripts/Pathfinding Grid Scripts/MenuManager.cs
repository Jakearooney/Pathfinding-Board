using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PathfindingTesterScript pathfindTest;

    [SerializeField] private GameObject mainCamera;

    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;

    [SerializeField] private GameObject pathfindBoard;
    [SerializeField] private GameObject mainMenu;

    private void Start()
    {
        UpdateText();
    }

    public void ChangeHeight(int value)
    {
        if (value == +1 && pathfindTest.setGridHeight < 14 || value == -1 && pathfindTest.setGridHeight > 1)
        {
            pathfindTest.setGridHeight += value;
            UpdateText();
        }
        
    }

    public void ChangeWidth(int value)
    {
        if (value == +1 && pathfindTest.setGridWidth < 25 || value == -1 && pathfindTest.setGridWidth > 1)
        {
            pathfindTest.setGridWidth += value;
            UpdateText();
        }
        
    }

    private void UpdateText()
    {
        heightText.text = pathfindTest.setGridHeight.ToString();
        widthText.text = pathfindTest.setGridWidth.ToString();
    }

    public void createBoard()
    {
        pathfindBoard.active = true;
        mainMenu.active = false;
        mainCamera.transform.position = new Vector3(pathfindTest.setGridWidth * 10 / 2, pathfindTest.setGridHeight * 10 / 2, -50);
    }
}
