using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 70f;
    public GameObject player;
    bool m_IsPlayerAtExit;
    
    float m_Timer;
    public GameObject exitBackgroundObject;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public GameObject levelMusic;

    bool m_HasAudioPlayed;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("player reached the end ");
            m_IsPlayerAtExit = true;
            exitBackgroundObject.SetActive(true);
            levelMusic.SetActive(false);
        }




    }


    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }

      
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {


            Debug.Log("game over");
            Application.Quit();

            

        }

    }

}