using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    float respawnDelay = 0.5f;

    public bool explanationTime;
    public GameObject explPanel;
    public Text[] scoreText;
    public GameObject winnerUI, pausePanel, pauseButton, musicButton;

    [HideInInspector]
    public int score = 0;
    [HideInInspector]
    public int totalScore;

    MainMenu mainMenu;

    public Button soundButton;
    public Sprite soundOn, soundOff;
    public AudioSource music;

    bool soundisActive;
    int boolingActive;
    bool isMuted;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        mainMenu = FindObjectOfType<MainMenu>();
        pauseButton.SetActive(true);
        musicButton.SetActive(true);

        if (PlayerPrefs.HasKey("totalScore"))
        {
            totalScore = PlayerPrefs.GetInt("totalScore");
        }
        if (PlayerPrefs.HasKey("soundActive"))
        {
            boolingActive = PlayerPrefs.GetInt("soundActive");
        }
        if (boolingActive == 0)
        {
            music.mute = false;
            soundisActive = false;
        }
        if (boolingActive == 1)
        {
            music.mute = true;
            soundisActive = true;
        }
    }

    private void Update()
    {
        if (!soundisActive)
        {
            soundButton.image.sprite = soundOn;
            PlayerPrefs.SetInt("soundActive", 0);
            music.mute = false;
        }
        if (soundisActive)
        {
            soundButton.image.sprite = soundOff;
            PlayerPrefs.SetInt("soundActive", 1);
            music.mute = true;
        }

        if (explanationTime)
        {
            explPanel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                explanationTime = false;
            }
        }
        if (!explanationTime)
        {
            explPanel.SetActive(false);
        }
    }


    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
    }

    public IEnumerator RespawnCoroutine()
    {
        playerController.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //playerController.transform.position = playerController.respawnPoint;
        //playerController.gameObject.SetActive(true);
    }

    public void AddScore(int numberOfScore)
    {
        score += numberOfScore;
        scoreText[0].text = score.ToString();
    }

    public void ScoreMarker()
    {
        totalScore += (score / 10);
        scoreText[2].text = "Yýldýz Sayýsý: " + totalScore.ToString();
        PlayerPrefs.SetInt("totalScore", totalScore);
    }

    public int ScoreChanger()
    {
        if (PlayerPrefs.HasKey("totalScore"))
        {
            totalScore = PlayerPrefs.GetInt("totalScore");
        }
        return totalScore;
    }

    public void ScoreSaver(int pointOfScore)
    {
        totalScore -= pointOfScore;
        PlayerPrefs.SetInt("totalScore", totalScore);
    }

    public void LevelUp() 
    {
        winnerUI.SetActive(true);
        pauseButton.SetActive(false);
        musicButton.SetActive(false);
        scoreText[1].text = "Toplam Skor: " + score.ToString();
    }

    public void PauseMenu(bool isActive)
    {
        if (isActive)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
        }
        
    }

    public void ExplanationPanel()
    {
        explanationTime = false;
    }

    public void GotoMenu()
    {
        PauseMenu(false);
        SceneManager.LoadScene("Menu");
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PauseMenu(false);
    }

    public void LevelIndex()
    {
        mainMenu.LevelUpper();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SoundModeChanger()
    {
        isMuted = !isMuted;  
        soundisActive = !soundisActive;
    }
}
