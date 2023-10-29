
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    float pushForce = 1000f;

    float movement;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float jumpPower;
    bool isJumping = true;

    float timer = 3.3f;
    public Vector3 respawnPoint;
    private GameController gameController;
    public Text sayimText;
    public GameObject scoreText;

    
    void Sayici()
    {
        if (!gameController.explanationTime)
        {
            if (timer >= 1)
            {
                timer -= Time.deltaTime;

                sayimText.text = timer.ToString();

            }
            else
            {
                Destroy(sayimText);
                scoreText.SetActive(true);
                gameController.explanationTime = false;
            }
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        respawnPoint = transform.position;
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        Sayici();

        if (isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (timer <= 1)
        {
            rb.AddForce(0, 0, pushForce * Time.fixedDeltaTime);
            rb.velocity = new Vector3(movement * speed, rb.velocity.y, rb.velocity.z);
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bareer"))
        {
            // Time.timeScale = 0.5f;           
            gameController.RespawnPlayer();
        }
        if (other.collider.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "endTrigger")
        {
            gameController.ScoreMarker();
            gameController.LevelUp();
            gameController.LevelIndex();
            this.gameObject.SetActive(false);
            
        }
        if (other.name == "FallDetector")
        {
            gameController.RespawnPlayer();
        }
        
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isJumping = false;
    }

   
}
