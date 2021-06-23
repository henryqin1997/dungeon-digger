using UnityEngine;

public abstract class FocusBehaviour : MonoBehaviour
{
    public abstract void FocusAcquired(string acquiredFocusId);
    public abstract void FocusReleased(string releasedFocusId);
}
