using UnityEngine;
using UnityEngine.UI;

public class FocusButton : FocusBehaviour
{
    public string            focusId;
    public AcquireFocusEvent acquireFocusEvent;
    public ReleaseFocusEvent releaseFocusEvent;
    public Sprite            focusOffSprite;
    public Sprite            focusOnSprite;

    // Start is called before the first frame update
    void Start()
    {
        SetSprite(focusOffSprite);
    }

    public void OnClick()
    {
        if (IsFocused()) {
            ReleaseFocus();
        } else {
            AcquireFocus();
        }
    }

    public void AcquireFocus()
    {
        acquireFocusEvent.Invoke(focusId);
    }

    public void ReleaseFocus()
    {
        releaseFocusEvent.Invoke(focusId);
    }

    public override void FocusAcquired(string acquiredFocusId)
    {
        Sprite nextSprite = (acquiredFocusId == focusId)
                          ? focusOnSprite
                          : focusOffSprite;
        SetSprite(nextSprite);
    }

    public override void FocusReleased(string releasedFocusId)
    {
        if (releasedFocusId == focusId)
        {
            SetSprite(focusOffSprite);
        }
    }

    private void SetSprite(Sprite sprite)
    {
        GetImage().sprite = sprite;
    }

    private bool IsFocused()
    {
        return GetImage().sprite == focusOnSprite;
    }

    private Image GetImage()
    {
        return gameObject.GetComponent<Image>();
    }
}
