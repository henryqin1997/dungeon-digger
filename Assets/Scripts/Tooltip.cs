﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    [SerializeField]
    private Camera uiCamera;

    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.Find("Text").GetComponent<Text>();

        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    public void ShowTooltip(string tooltipString)
    {
        SetChildrenActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 6f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth  + textPaddingSize * 2f,
                                             tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
        Update();
    }

    public void HideTooltip()
    {
        SetChildrenActive(false);
    }

    private void SetChildrenActive(bool active)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
