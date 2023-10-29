using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateCoin : MonoBehaviour
{
    public int scoreValue = 0;
    GameController gameController;
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 180f * Time.deltaTime);
        gameController = FindObjectOfType<GameController>();

    }

    private void OnTriggerEnter(Collider other)
    {    
        if (other.CompareTag("Player"))
        {
            gameController.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

    
}
