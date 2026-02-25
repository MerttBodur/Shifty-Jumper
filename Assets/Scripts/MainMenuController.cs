using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    [Header("Level Ayarlarý")]
    public string firstLevelName = "Level1";

    private const string LastLevelKey = "LastLevel";

    public void OnClick_Start() // START butonuna atanacak
    {
        PlayerPrefs.DeleteKey(LastLevelKey); // Son savei sýfýrla

        PlayerController.deathCount = 0; // death countý sýfýrla // Coin sayýsýný sýfýrlamaya gerek yok zaten default olarak 0

        SceneManager.LoadScene(firstLevelName);

    }

    public void OnClick_Load() // LOAD butonuna atanacak
    {
        if (PlayerPrefs.HasKey(LastLevelKey))
        {
            string lastlevel = PlayerPrefs.GetString(LastLevelKey); // daha önce kaydedilen bir save varsa onu GetString ile al
            SceneManager.LoadScene(lastlevel);  // O sahneyi yükle
        }
        else
        {
            SceneManager.LoadScene(firstLevelName); // yok ise ilk sahneyi yükle
        }
    }

    // QUIT: Oyunu kapat
    public void OnClick_Quit()
    {
#if UNITY_EDITOR
        // Editörde deniyorsan play moddan çýksýn
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Build'de oyunu kapat
        Application.Quit();
#endif
    }

    // Bu static helper'ý gameplay tarafýnda çaðýracaðýz
    public static void SaveCurrentLevel()
    {
        var scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString(LastLevelKey, scene.name);
        PlayerPrefs.Save();
    }
}