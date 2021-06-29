using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    void Start()
    {
    }

    // Update is called once per frame
    public void SetItems(IItem item, int count)
    {
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
