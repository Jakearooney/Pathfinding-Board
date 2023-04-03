using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class HeatmapMenuManager : MonoBehaviour
{
    [SerializeField] private HeatmapTestingScript heatmapTest;

    [SerializeField] private GameObject mainCamera;

    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;

    [SerializeField] private GameObject pathfindBoard;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private int currentScene;

    private void Start()
    {
        HeatmapUpdateText();
    }

    public void HeatmapTestChangeHeight(int value)
    {
        if (value == +1 && heatmapTest.setGridHeight < 36 || value == -1 && heatmapTest.setGridHeight > 1)
        {
            heatmapTest.setGridHeight += value;
            HeatmapUpdateText();
        }
    }

    public void HeatmapTestChangeWidth(int value)
    {
        if (value == +1 && heatmapTest.setGridWidth < 68 || value == -1 && heatmapTest.setGridWidth > 1)
        {
            heatmapTest.setGridWidth += value;
            HeatmapUpdateText();
        }
    }

    private void HeatmapUpdateText()
    {
        heightText.text = heatmapTest.setGridHeight.ToString();
        widthText.text = heatmapTest.setGridWidth.ToString();
    }

    public void createBoard()
    {
        pathfindBoard.active = true;
        mainMenu.active = false;
        mainCamera.transform.position = new Vector3(heatmapTest.setGridWidth * 10 / 4, heatmapTest.setGridHeight * 10 / 4, -50);
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
