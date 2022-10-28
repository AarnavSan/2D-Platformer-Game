using UnityEngine;
using Cinemachine;

public class HammerController : MonoBehaviour
{
    [SerializeField] private AudioClip pistonBang;
    [SerializeField] private int rangeOfPlayer = 5;
    private CinemachineImpulseSource source;
    private GameManager gameManager;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        source = GetComponent<CinemachineImpulseSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void ShakeScreen()
    {
        if(Mathf.Abs(Mathf.Abs(this.transform.position.x) - Mathf.Abs(gameManager.GetPlayerPosition().x)) <= rangeOfPlayer)
        {
            source.GenerateImpulse();
            audioSource.PlayOneShot(pistonBang);
        }
    }
}