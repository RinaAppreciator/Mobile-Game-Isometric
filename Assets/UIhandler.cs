using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIhandler : MonoBehaviour
{

    public VisualElement m_healthbar;
    private VisualElement m_NonPlayerDialogue;
    public float displayTime = 4.0f;
    private float m_TimerDisplay;
    public static UIhandler instance { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        m_NonPlayerDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCdialogue");
        SetHealthValue(1f);
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

    }

    public void SetHealthValue(float width)
    {
        m_healthbar.style.width = Length.Percent(width * 100.0f);
    }

    public void DialogueVisibility()
    {

        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
            {
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
            }
        }
    }
}
