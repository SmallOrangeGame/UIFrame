using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFormMono : UIPrefabMono
{
    private RectTransform m_rectTransform;
    private Canvas m_canvas;
    private List<UILayer> m_autoLayers;

    private string m_layerName = string.Empty;
    public Canvas mCanvas => m_canvas;

    void Awake()
    {
        //����ʱ���㼶��ΪHome
        m_layerName = "Home";
        transform.localScale = Vector3.zero;
        m_rectTransform = GetComponent<RectTransform>();
        m_canvas = GetComponent<Canvas>();
        m_autoLayers = new List<UILayer>();

        //�����������
        m_rectTransform.anchorMin = Vector2.zero;
        m_rectTransform.anchorMax = Vector2.one;
        m_rectTransform.anchoredPosition3D = Vector3.zero;
        m_rectTransform.offsetMin = Vector2.zero;
        m_rectTransform.offsetMax = Vector2.zero;
        m_rectTransform.pivot = Vector3.one * 0.5f;
        m_rectTransform.localScale = Vector3.one;

        SetLayer(m_layerName);
    }

    private void OnEnable()
    {
        SetLayer(m_layerName);
    }
    //������ʾ�㼶
    public void SetLayer(string layerName)
    {
        m_layerName = layerName;
        if (m_canvas != null)
            m_canvas.sortingLayerName = layerName;
        foreach (var p in m_autoLayers)
            p.Refresh();
    }

    //������ʾ�����
    public void SetSortOrder(int sortOrder)
    {
        if (m_canvas == null)
            return;
        if (!m_canvas.overrideSorting)
            m_canvas.overrideSorting = true;
        m_canvas.sortingOrder = sortOrder;
        foreach (var p in m_autoLayers)
            p.Refresh();
    }
    //��ӵ��Զ������㼶���б�
    public void AddAutoLayer(UILayer autoLayer)
    {
        if (!m_autoLayers.Contains(autoLayer))
            m_autoLayers.Add(autoLayer);

        autoLayer.Refresh();
        if (!IsShow())
            autoLayer.Hide();
        else
            autoLayer.Show();
    }
    //���Զ������㼶���б����Ƴ�
    public void RemoveAutoLayer(UILayer autoLayer)
    {
        if (m_autoLayers.Contains(autoLayer))
            m_autoLayers.Remove(autoLayer);
    }

    public bool IsShow()
    {
        return gameObject.activeSelf && m_canvas.enabled;
    }
}
