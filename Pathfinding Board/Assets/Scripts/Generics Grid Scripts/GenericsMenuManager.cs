using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GenericsMenuManager : MonoBehaviour
{
    [SerializeField] private GenericsTestingScript genericsTest;

    [SerializeField] private GameObject mainCamera;

    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;

    [SerializeField] private GameObject pathfindBoard;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private int currentScene;

    private void Start()
    {
        GenericsUpdateText();
    }

    public void GenericsTestChangeHeight(int value)
    {
        if (value == +1 && genericsTest.setGridHeight < 9 || value == -1 && genericsTest.setGridHeight > 1)
        {
            genericsTest.setGridHeight += value;
            GenericsUpdateText();
        }
    }

    public void GenericsTestChangeWidth(int value)
    {
        if (value == +1 && genericsTest.setGridWidth < 16 || value == -1 && genericsTest.setGridWidth > 1)
        {
            genericsTest.setGridWidth += value;
            GenericsUpdateText();
        }
    }

    private void GenericsUpdateText()
    {
        heightText.text = genericsTest.setGridHeight.ToString();
        widthText.text = genericsTest.setGridWidth.ToString();
    }

    public void createBoard()
    {
        pathfindBoard.active = true;
        mainMenu.active = false;
        mainCamera.transform.position = new Vector3(genericsTest.setGridWidth * 10 / 2 - 8, genericsTest.setGridHeight * 10 / 2 - 3, -50);
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
