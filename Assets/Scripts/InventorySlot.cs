using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    // Update is called once per frame
    public void SetItems(IItem item, int count)
    {
        SetFrame(item.GetFrame());
        SetIcon( item.GetIcon());
        SetCount(count);
    }

    public void SetSelectable(bool selectable)
    {
        GetIcon().GetComponent<Button>().interactable = interactable;
    }

    private void SetFrame(Sprite frame)
    {
        GetDescendantComponent<Image>("Frame").sprite = frame;
    }

    private void SetIcon(Sprite icon)
    {
        GetIcon().sprite = icon;
    }

    private GameObject GetIcon()
    {
        return GetDescendantComponent<Image>("Icon");
    }

    private void SetCount(int count)
    {
        Debug.Assert(count > 0);
        GetDescendantComponent<Text>("Count").text = count.ToString();
    }

    private T GetDescendantComponent<T>(string path)
    {
        Transform transform = gameObject.transform.Find(path);
        Debug.Assert(transform != null);
        return transform.gameObject.GetComponent<T>();
    }
}
