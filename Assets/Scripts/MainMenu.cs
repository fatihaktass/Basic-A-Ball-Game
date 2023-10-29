using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelsPanel, storePanel, infoPanel;
    public Button[] levelButtons;
    static int levelCount;

    bool onClickToInfoButton;
    public Material playerMaterial;
    public Text pointInfo;
    public GameController gameController;
    public SelectedButton selectedButton;
    public BuyAnItem buyAi;

    private void Start()
    {
        selectedButton = FindObjectOfType<SelectedButton>();
        gameController = FindObjectOfType<GameController>();
        buyAi = FindObjectOfType<BuyAnItem>();
        if (PlayerPrefs.HasKey("LevelRemember"))
        {
            levelCount = PlayerPrefs.GetInt("LevelRemember");
        }

        for (int i = levelCount; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
        
    }

    private void Update()
    {
        levelButtons[levelCount].interactable = levelCount switch
        {
            _ => true, 
        } ;

        if (levelsPanel)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                levelsPanel.SetActive(false);
            }
        }
       
 
    }

    public void ChooseLevel()
    {
        levelsPanel.SetActive(true);
    }

    public void OnClickINFOButton()
    {
        onClickToInfoButton =! onClickToInfoButton;
        if (onClickToInfoButton)
        {
            infoPanel.SetActive(true);
        }
        else
        {
            infoPanel.SetActive(false);
        }
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level01");
        levelsPanel.SetActive(false);
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level02"); 
        levelsPanel.SetActive(false);
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level03");
        levelsPanel.SetActive(false);
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level04");
        levelsPanel.SetActive(false);
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void OpenStore(bool isActive)
    {
        if (isActive)
        {
            storePanel.SetActive(true);
           
            selectedButton.DisableButtonStarting();
        }
        else
        {
            storePanel.SetActive(false);
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void LevelUpper()
    {
        if (levelCount < SceneManager.GetActiveScene().buildIndex)
        {
            PlayerPrefs.SetInt("LevelRemember", SceneManager.GetActiveScene().buildIndex);
        }  
    }

    
}
