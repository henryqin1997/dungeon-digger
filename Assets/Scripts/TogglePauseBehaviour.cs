using UnityEngine;

public class TogglePauseBehaviour : MonoBehaviour
{
    public void TogglePause()
    {
        Time.timeScale = (Time.timeScale == 0.0f) ? 1.0f : 0.0f;
    }
}
