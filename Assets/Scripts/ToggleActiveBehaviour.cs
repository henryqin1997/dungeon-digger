using UnityEngine;

public class ToggleActiveBehaviour : MonoBehaviour
{
    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
