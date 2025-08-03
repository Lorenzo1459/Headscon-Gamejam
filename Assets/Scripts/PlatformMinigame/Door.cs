using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour

{
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().EndLevel();
        }
    }
}
