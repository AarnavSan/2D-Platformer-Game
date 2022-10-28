using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    //Inspector Referenced Objects
    public TextMeshProUGUI moneyCounter;
    public AudioClip walkingFootsteps;
    public AudioClip jumpSound;
    public AudioClip moneyPickupSound;
    public AudioClip objectivePickupSound;
    public GameObject fadeOutOverPlayerTextPrefab;
    public Transform textSpawnLocation;
    public GameManager gameManager;

    //public variables
    public float walkToIdleOffset = 0.1f;
    public bool isJumping;
    public bool completedObjective;

    //private variables
    AudioSource audioSource;
    CharacterController2D controller;
    Animator animator;
    Rigidbody2D rb;
    PlayerMovement playerMovement;
    int money = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController2D>();
        playerMovement = GetComponent<PlayerMovement>();
        completedObjective = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            SpawnTextOverPlayer("Hello there!");
        }
        UpdateAnimation();
        UpdateSoundOutput();
    }

    //Update Animator parameters
    void UpdateAnimation()
    {
        animator.SetBool("walking", Math.Abs(rb.velocity.x) > walkToIdleOffset);
    }

    //Check Collision with Triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if collided with money add 1 to money counter
        if (collision.CompareTag("money"))
        {
            Destroy(collision.gameObject);
            money += 1;
            PlayAudioOnPlayer(moneyPickupSound);
            UpdateUI();
        }
        else if (collision.CompareTag("killzone"))
        {
            PlayerDied();
        }
        else if (collision.CompareTag("hammer"))
        {
            PlayerDied();
        }
        else if(collision.CompareTag("objective"))
        {
            completedObjective = true;
            audioSource.PlayOneShot(objectivePickupSound);
            Destroy(collision.gameObject);
        }  
    }

    //Update user interface
    private void UpdateUI()
    {
        moneyCounter.text = money.ToString();
    }

    //getter for player money
    public int GetMoney()
    {
        return money;
    }

    //Update sounds made by player mainly footsteps
    private void UpdateSoundOutput()
    {
        if (controller.IsPlayerGrounded() && Math.Abs(rb.velocity.x) > walkToIdleOffset)
        {
            audioSource.clip = walkingFootsteps;
        }
        if (!audioSource.isPlaying)
        {
            if (controller.IsPlayerGrounded() && Math.Abs(rb.velocity.x) > walkToIdleOffset)
            {
                audioSource.PlayOneShot(walkingFootsteps);
            }
            else
                audioSource.Stop();
        }
        if (isJumping && controller.IsPlayerGrounded())
        {
            PlayAudioOnPlayer(jumpSound);
        }
    }

    public void PlayAudioOnPlayer(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void SpawnTextOverPlayer(string textToDisplay)
    {
        GameObject textDisplayed = Instantiate(fadeOutOverPlayerTextPrefab, textSpawnLocation);
        textDisplayed.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //textDisplayed.GetComponent<Canvas>().worldCamera = Camera.main;
        textDisplayed.GetComponentInChildren<TextMeshProUGUI>().text = textToDisplay;
        Destroy(textDisplayed, 2.0f);
    }

    private void PlayerDied()
    {
        SetPlayerMovement(false);
        gameManager.PlayerRespawn();
    }

    public void SetPlayerMovement(bool value)
    {
        playerMovement.enableMovement = value;
    }
}
