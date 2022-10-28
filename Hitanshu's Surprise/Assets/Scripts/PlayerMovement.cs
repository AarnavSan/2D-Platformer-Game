using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Inspector Referenced Objects
    public CharacterController2D controller;

    //Public variables
    public float runSpeed = 40f;
    public bool enableMovement = true;
    //Private variables
    float horizontalMove = 0f;
    bool jump = false;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            player.isJumping = true;
        }

        if (enableMovement)
        {
            UpdateMovement();
        }
    }

    //Fixed Update is called a fixed number of times each second
    private void FixedUpdate()
    {
        //Check value of horizontal input axis
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime;
    }

    //Update Movement
    void UpdateMovement()
    {
        controller.Move(horizontalMove, false, jump);
        jump = false;
        player.isJumping = false;
    }
}