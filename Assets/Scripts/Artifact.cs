using UnityEngine;

public abstract class Artifact : MonoBehaviour
{
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Character")
        {
            OnPickUp();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickUp();
}
