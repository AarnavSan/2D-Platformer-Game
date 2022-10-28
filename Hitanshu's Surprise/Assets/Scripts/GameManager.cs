using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform initialPlayerSpawn;
    [SerializeField] AudioClip finishAudioClip;

    Player player;
    Transform lastCheckpoint;
    string[] disrespectText;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseDisrespect();
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
            player.SetPlayerMovement(true);
            player.gameManager = this;
            lastCheckpoint = initialPlayerSpawn;
            //PlayerRespawn();
        }
        catch
        {
            Debug.Log("No player in Scene");
        }
    }

    private void InitialiseDisrespect()
    {
        disrespectText = new string[]{
            "Bruh how are you so bad at this game",
            ".....dissapointing",
            "Don't worry JOD i'll respawn you somewhere safe",
            "Try again JOD, you'll finish this easily",
            "This game is easy for you bro",
            "its ok bro you are jod"};
    }

    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void UpdateLastCheckpointTransform(Transform lastCheckpointTransform)
    {
        lastCheckpoint = lastCheckpointTransform;
    }

    public void PlayerRespawn()
    {
        player.transform.position = lastCheckpoint.position;
        player.SetPlayerMovement(true);
        player.SpawnTextOverPlayer(disrespectText[Random.Range(0, disrespectText.Length)]);
    }

    //General Functions
    public void FinishLevel()
    {
        player.PlayAudioOnPlayer(finishAudioClip);
        Invoke("LoadNextScene", 10.0f);
    }

    public void LoadNextScene()
    {
        LoadSceneIndex(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadSceneIndex(int n)
    {
        SceneManager.LoadSceneAsync(n);
    }

    public void LoadSavedScene()
    {
        LoadSceneIndex(PlayerPrefs.GetInt("Level"));
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }
}
