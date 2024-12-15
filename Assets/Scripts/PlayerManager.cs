using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechXR.Core.Sense;

public class PlayerManager : MonoBehaviour
{
    public Shooting shooting;
    private Health_spawner healthSpawner;
    public Transform startPosition;
    public GameObject laserPointer, MainMenu, startGameAssets;
    public SenseController senseController;
    public Transform enemyContainer;
    private Health playerHealth;
    public ScoreManager scoreManager;
    public AudioClip healthSound;

    bool inGame;
    // Start is called before the first frame update
    
    void Start()
    {
        playerHealth = GetComponent<Health>();
        healthSpawner = FindObjectOfType<Health_spawner>();

        startPosition.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(SenseInput.GetButton(ButtonName.L) && inGame)
        {
            shooting.Shoot();   
        }

        if(playerHealth.health <= 0)
        {
            GameOver();
        }
    }

    public void GameStart()
    {
        inGame = true;
        MainMenu.SetActive(false);

        senseController.ToggleJoystickMovement(true);
        shooting.gameObject.SetActive(true);

        laserPointer.gameObject.SetActive(false);
        startGameAssets.SetActive(true);
        scoreManager.ResetScore();
    }

    public void GameOver()
    {
        inGame = false;
        MainMenu.SetActive(true);

        senseController.ToggleJoystickMovement(false);
        shooting.gameObject.SetActive(false);

        laserPointer.gameObject.SetActive(true);
        laserPointer.GetComponent<LaserPointer>().ButtonState = false;

        playerHealth.ResetHealth();

        transform.position = startPosition.position;

        foreach (Transform child in enemyContainer)
        {
            child.gameObject.GetComponent<Enemy_AI>().Enemy_Death();
        }
        
        startGameAssets.SetActive(false);

        scoreManager.SetHighScore();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("health"))
        {
            playerHealth.Heal(10);
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().PlayOneShot(healthSound); //plays the audio on collision with the coin

            // Call HealthPickupCollected function of the Health_spawner script
            FindObjectOfType<Health_spawner>().HealthPickupCollected();
        }
    }

}
