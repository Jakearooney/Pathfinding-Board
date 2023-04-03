using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class PathfindingMenuManager : MonoBehaviour
{
    [SerializeField] private PathfindingTesterScript pathfindTest;

    [SerializeField] private GameObject mainCamera;

    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;

    [SerializeField] private GameObject pathfindBoard;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private int currentScene;

    private void Start()
    {
        PathfindUpdateText();
    }

    public void PathfindTestChangeHeight(int value)
    {
        if (value == +1 && pathfindTest.setGridHeight < 12 || value == -1 && pathfindTest.setGridHeight > 1)
        {
            pathfindTest.setGridHeight += value;
            PathfindUpdateText();
        }
    }

    public void PathfindTestChangeWidth(int value)
    {
        if (value == +1 && pathfindTest.setGridWidth < 24 || value == -1 && pathfindTest.setGridWidth > 1)
        {
            pathfindTest.setGridWidth += value;
            PathfindUpdateText();
        }
    }

    private void PathfindUpdateText()
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

    public void PreviousScene()
    {
        currentScene += 1;
        SceneManager.LoadScene(currentScene);
    }

    public void NextScene()
    {
        currentScene -= 1;
        SceneManager.LoadScene(currentScene);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitBuild()
    {
        Application.Quit();
    }
}
