using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the mode scene menu
/// </summary>

public class ModeMenuManager : MonoBehaviour {


    private AudioSource clickSound;

    void Start()
    {
        clickSound = GetComponent<AudioSource>();
    }

    //method to be called when we press addition button
    public void EasyMode()
    {
        GameManager.singleton.currentMode = 1;
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void MediumMode()
    {
        GameManager.singleton.currentMode = 2;
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void HardMode()
    {
        GameManager.singleton.currentMode = 3;
        SceneManager.LoadScene("GamePlay");
        clickSound.Play();
    }

    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
        clickSound.Play();
    }
}
