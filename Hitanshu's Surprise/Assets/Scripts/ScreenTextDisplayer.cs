using UnityEngine;
using TMPro;

public class ScreenTextDisplayer : MonoBehaviour
{
    public string senderString;

    //TextArea [(min lines, max lines)]
    [TextArea(15, 20)]
    public string messageString;

    [SerializeField] Canvas textCanvas;
    [SerializeField] TextMeshProUGUI senderTextBox;
    [SerializeField] TextMeshProUGUI messageTextBox;

    private void Start()
    {
        textCanvas.worldCamera = Camera.main;
        senderTextBox.text = senderString;
        textCanvas.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        messageTextBox.text = messageString;
        if (collision.CompareTag("Player"))
        {
            textCanvas.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textCanvas.enabled = false;
        }
    }


}
