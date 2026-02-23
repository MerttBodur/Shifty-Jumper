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
        if (player.coin >= requiredCoin)
        {
            doorCollider.enabled = true;
        }
        else
        {
            doorCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.coin >= requiredCoin)
        {
            SceneManager.LoadScene("Level2");
        }
    }

}
