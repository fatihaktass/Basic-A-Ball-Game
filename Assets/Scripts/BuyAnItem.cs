using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyAnItem : MonoBehaviour
{
    GameController gameController;
    SelectedButton selectedButton;
    public Material pMaterial;
    public Text usingInfoText;
    public Text StarText;
    int colorRemember;

    [SerializeField]
    int starScore;

    

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        selectedButton = FindObjectOfType<SelectedButton>();
        StarText.text = gameController.ScoreChanger().ToString();

        if (PlayerPrefs.HasKey("colorText"))
        {
            usingInfoText.text = PlayerPrefs.GetString("colorText");
        }
        if (PlayerPrefs.HasKey("colorIndex"))
        {
            colorRemember = PlayerPrefs.GetInt("colorIndex");
        }

        switch (colorRemember)
        {
            case 0:
                pMaterial.color = Color.yellow;
                usingInfoText.color = Color.yellow;
                break;
            case 1:
                pMaterial.color = Color.blue;
                usingInfoText.color = Color.blue;
                break;
            case 2:
                pMaterial.color = Color.red;
                usingInfoText.color = Color.red;
                break;
            case 3:
                pMaterial.color = Color.magenta;
                usingInfoText.color = Color.magenta;
                break;
        }
    }

    public void OnButtonClick(int buttonIndex)
    {
        if (gameController.totalScore >= 5)
        {
            gameController.ScoreSaver(starScore);
            StarText.text = gameController.ScoreChanger().ToString();
            PlayerPrefs.SetInt("colorIndex", buttonIndex);
            selectedButton.SelectedButtonIndex(buttonIndex);
            switch (buttonIndex)
            {
                case 0:
                    pMaterial.color = Color.yellow;
                    usingInfoText.color = Color.yellow;
                    usingInfoText.text = "Sarý";
                    PlayerPrefs.SetString("colorText", "Sarý");
                    break;
                case 1:
                    pMaterial.color = Color.blue;
                    usingInfoText.color = Color.blue;
                    usingInfoText.text = "Mavi";
                    PlayerPrefs.SetString("colorText", "Mavi");
                    break;
                case 2:
                    pMaterial.color = Color.red;
                    usingInfoText.color = Color.red;
                    usingInfoText.text = "Kýrmýzý";
                    PlayerPrefs.SetString("colorText", "Kýrmýzý");
                    break;
                case 3:
                    pMaterial.color = Color.magenta;
                    usingInfoText.color = Color.magenta;
                    usingInfoText.text = "Mor";
                    PlayerPrefs.SetString("colorText", "Mor");
                    break;
            }
        }
        
    }
}
