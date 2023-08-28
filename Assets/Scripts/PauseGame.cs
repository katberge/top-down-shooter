using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite playSprite;

    public void TogglePause()
    {
        if (Time.timeScale == 0)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = playSprite;
    }

    private void Unpause()
    {
        Time.timeScale = 1;
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = pauseSprite;
    }
}
