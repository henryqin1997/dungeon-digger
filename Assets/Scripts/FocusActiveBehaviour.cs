using UnityEngine;

public class FocusActiveBehaviour : FocusBehaviour
{
    public string focusId;

    public override void FocusAcquired(string acquiredFocusId)
    {
        SetActive(acquiredFocusId == focusId);
    }

    public override void FocusReleased(string releasedFocusId)
    {
        if (releasedFocusId == focusId)
        {
            SetActive(false);
        }
    }

    private void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

}
