using System;
using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{

    public int selectedButton;
    public Button[] StoreButtons;

    private void Start()
    {
        if (PlayerPrefs.HasKey("selectedButtonIndex"))
        {
            selectedButton = PlayerPrefs.GetInt("selectedButtonIndex");
        }
        else
        {
            PlayerPrefs.SetInt("selectedButtonIndex", selectedButton);
        }
    }

    public void SelectedButtonIndex(int index)
    {
        selectedButton = index;
        PlayerPrefs.SetInt("selectedButtonIndex", selectedButton);
        for (int i = 0; i < StoreButtons.Length; i++)
        {
            StoreButtons[i].interactable = true;

        }
        StoreButtons[index].interactable = false;
    }

    public void DisableButtonStarting()
    {
        StoreButtons[selectedButton].interactable = false;
    }
}
