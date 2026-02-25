using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public int requiredCoin = 30;
    public Collider2D doorCollider;
    private PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (doorCollider == null)
            doorCollider = GetComponent<Collider2D>();

        doorCollider.enabled = false;
    }

    void Update()
    {
        // sadece yeterli coin varsa kapý aktif olsun
        doorCollider.enabled = isDoorOpen;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        if (!isDoorOpen) return;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // Güvenlik için: build settings'te gerçekten bir sonraki sahne var mý?
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("DoorController: Sonraki sahne bulunamadý. Build Settings sýrasýný kontrol et.");
        }
    }

    public bool isDoorOpen
    {
        get
        {
            return player != null && player.coin >= requiredCoin;
        }
    }
}


