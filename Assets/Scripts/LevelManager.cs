using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        // Sahne yüklendiðinde son level olarak kaydet
        string sceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastLevel", sceneName);
        PlayerPrefs.Save();

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.coin = 0;
        }
    }
}