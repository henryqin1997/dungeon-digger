using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AcquireFocusEvent : UnityEvent<string>
{}

[System.Serializable]
public class FocusAcquiredEvent : UnityEvent<string>
{}

[System.Serializable]
public class ReleaseFocusEvent : UnityEvent<string>
{}

[System.Serializable]
public class FocusReleasedEvent : UnityEvent<string>
{}

public class FocusManager : MonoBehaviour
{
    public UnityEvent       focusAcquiredEvent;
    public UnityEvent       focusReleasedEvent;
    public FocusBehaviour[] focusBehaviours;

    public void AcquireFocus(string focusId)
    {
        foreach (FocusBehaviour focusBehaviour in focusBehaviours) {
            focusBehaviour.FocusAcquired(focusId);
        }

        focusAcquiredEvent.Invoke();
    }

    public void ReleaseFocus(string focusId)
    {
        foreach (FocusBehaviour focusBehaviour in focusBehaviours) {
            focusBehaviour.FocusReleased(focusId);
        }

        focusReleasedEvent.Invoke();
    }
}
