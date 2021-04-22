using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject towerShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    //[SerializeField] private GameObject levelSign;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI towerLevelText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;
    //[SerializeField] private TextMeshProUGUI levelSign;

    [SerializeField] private TextMeshProUGUI victoryCoinsText;
    [SerializeField] private TextMeshProUGUI vLivesText;
    [SerializeField] private TextMeshProUGUI score;


    int sceneIndex, levelPassed;

    private Node currentNodeSelected;


    private void Start()
    {
        //levelSign = GameObject.Find("LevelNumber");

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
    }



    private void Update()
    {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        lifesText.text = LevelManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave}";

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowGameMenuPanel();
        }
     }

    public void StopTime()
    {
        Time.timeScale = 0f;
    }

    public void SlowTime()
    { 
        Time.timeScale = 0.5f;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public void FastTime()
    {
        Time.timeScale = 2f;
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
        victoryCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        vLivesText.text = LevelManager.Instance.TotalLives.ToString();
        score.text = LevelManager.Instance.Score.ToString();

        if (levelPassed < sceneIndex)
        {
            PlayerPrefs.SetInt("LevelPassed", sceneIndex);
        }

    }

    public void ShowGameMenuPanel()
    {       
            gameMenuPanel.SetActive(true);
            gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
            vLivesText.text = LevelManager.Instance.TotalLives.ToString();
            StopTime();
    }

    public void CloseGameMenuPanel()
    {
        gameMenuPanel.SetActive(false);
        ResumeTime();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeTime();
    }


    public void OpenAchievementPanel(bool status)
    {
        achievementPanel.SetActive(status);
    }

    public void OpenOptionsMenu(bool status)
    { 
        optionsPanel.SetActive(status);
    }



    public void CloseNodeUIPanel()
    {
        currentNodeSelected.CloseAttackRange();
        nodeUIPanel.SetActive(false);
    }


    public void CloseTowerShopPanel()
    {
        towerShopPanel.SetActive(false);
    }

    public void SellTower()
    {
        currentNodeSelected.SellTower();
        currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }

    public void UpgradeTower()
    {
        currentNodeSelected.Tower.TowerUpgrade.UpgradeTower();
        UpdateUpgradeText();
        UpdateTowerLevel();
        UpdateSellValue();
    }

    private void ShowNodeUI()
    {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateTowerLevel();
        UpdateSellValue();
    }

    private void UpdateUpgradeText()
    {
        upgradeText.text = currentNodeSelected.Tower.TowerUpgrade.UpgradeCost.ToString();
    }

    private void UpdateTowerLevel()
    {
        towerLevelText.text = $"Level {currentNodeSelected.Tower.TowerUpgrade.Level}";
    }

    private void UpdateSellValue()
    {
        int sellAmount = currentNodeSelected.Tower.TowerUpgrade.GetSellValue();
        sellText.text = sellAmount.ToString();
    }

    private void NodeSelected(Node nodeSelected)
    {
        currentNodeSelected = nodeSelected;
        if (currentNodeSelected.IsEmpty())
        {
            towerShopPanel.SetActive(true);
        }
        else 
        {
            ShowNodeUI();
        }
    }


    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
