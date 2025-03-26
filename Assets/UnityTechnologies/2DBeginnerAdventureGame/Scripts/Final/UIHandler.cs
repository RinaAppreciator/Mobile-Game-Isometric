using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    public float displayTime = 4.0f;
    
    private VisualElement m_Healthbar;
    private VisualElement m_DialogWindow;
    private float m_TimerDisplay;
    
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("Healthbar");

        m_DialogWindow = uiDocument.rootVisualElement.Q<VisualElement>("DialogWindow");
        m_DialogWindow.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        SetHealthValue(1.0f);
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_DialogWindow.style.display = DisplayStyle.None;
            }
        }
    }

    public void DisplayDialog()
    {
        m_DialogWindow.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }
}
