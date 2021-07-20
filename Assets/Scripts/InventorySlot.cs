using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Tooltip tooltip;
    private string  hoverText;

    private void Awake()
    {
        tooltip = Tooltip.FindTooltip();
    }

    private void OnDisable()
    {
        HideHoverText();
    }

    public void OnPointerEnter()
    {
        ShowHoverText();
    }

    public void OnPointerExit()
    {
        HideHoverText();
    }

    public void SetItems(IItem item, int count)
    {
        hoverText = item.GetDisplayName();
        SetFrame(item.GetFrame());
        SetIcon( item.GetIcon());
        SetCount(count);
    }

    public void SetSelectable(bool selectable)
    {
        GetButton().interactable = selectable;
    }

    public void SetSelectCallback(UnityAction call)
    {
        UnityEvent onClick = GetButton().onClick;
        onClick.RemoveAllListeners();
        onClick.AddListener(call);
    }

    private void ShowHoverText()
    {
        Debug.Assert(tooltip != null);
        Debug.Assert(hoverText != null);
        tooltip.ShowTooltip(hoverText);
    }

    private void HideHoverText()
    {
        Debug.Assert(tooltip != null);
        tooltip.HideTooltip();
    }

    private Button GetButton()
    {
        return gameObject.GetComponent<Button>();
    }

    private void SetFrame(Sprite frame)
    {
        GetDescendantComponent<Image>("Frame").sprite = frame;
    }

    private void SetIcon(Sprite icon)
    {
        GetDescendantComponent<Image>("Icon").sprite = icon;
    }

    private void SetCount(int count)
    {
        Debug.Assert(count > 0);
        GetDescendantComponent<Text>("Count").text = count.ToString();
    }

    private T GetDescendantComponent<T>(string path)
    {
        return GetDescendant(path).GetComponent<T>();
    }

    private GameObject GetDescendant(string path)
    {
        Transform transform = gameObject.transform.Find(path);
        Debug.Assert(transform != null);
        return transform.gameObject;
    }
}
