using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GridMenuManager : MonoBehaviour
{
    [SerializeField] private TestingScript gridTest;

    [SerializeField] private GameObject mainCamera;

    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;

    [SerializeField] private GameObject pathfindBoard;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private int currentScene;

    private void Start()
    {
        GridUpdateText();
    }

    public void GridTestChangeHeight(int value)
    {
        if (value == +1 && gridTest.setGridHeight < 4 || value == -1 && gridTest.setGridHeight > 1)
        {
            gridTest.setGridHeight += value;
            GridUpdateText();
        }
    }

    public void GridTestChangeWidth(int value)
    {
        if (value == +1 && gridTest.setGridWidth < 9 || value == -1 && gridTest.setGridWidth > 1)
        {
            gridTest.setGridWidth += value;
            GridUpdateText();
        }
    }

    private void GridUpdateText()
    {
        heightText.text = gridTest.setGridHeight.ToString();
        widthText.text = gridTest.setGridWidth.ToString();
    }

    public void createBoard()
    {
        pathfindBoard.active = true;
        mainMenu.active = false;
        mainCamera.transform.position = new Vector3(0, 0, -50);
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
