using UnityEngine;

public class CoinSystem : MonoBehaviour
{
    public GameObject effect;
    public AudioClip pickup;
    public float volume = 3f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // PlayerController'ý çarpýþan objeden veya parent'tan bul
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player == null) player = collision.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            Debug.LogError("CoinSystem: PlayerController bulunamadý. Hit = " + collision.name);
            return;
        }

        Instantiate(effect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(pickup, transform.position, volume);
        player.coin++;
        Destroy(gameObject);

    }
}
