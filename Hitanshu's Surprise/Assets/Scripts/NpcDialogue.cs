using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    //TextArea [(min lines, max lines)]
    [TextArea(5, 20)]
    public string objectiveCompleted;

    [TextArea(5, 20)]
    public string objectiveLeft;

    private ScreenTextDisplayer screenTextDisplayer;
    private GameManager gameManager;

    private void Start()
    {
        screenTextDisplayer = GetComponent<ScreenTextDisplayer>();
        screenTextDisplayer.messageString = objectiveLeft;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>().completedObjective)
            {
                screenTextDisplayer.messageString = objectiveCompleted;
                
                gameManager.FinishLevel();
            }
            else
            {
                screenTextDisplayer.messageString = objectiveLeft;
            }                
        }
    }
}
