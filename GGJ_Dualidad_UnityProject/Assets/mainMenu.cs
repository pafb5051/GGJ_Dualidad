using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public GameObject home;
    public GameObject levelSelect;
    

    public void Iniciar()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        home.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void Level1()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        SceneManager.LoadScene("SampleScene");  
    }

    public void Level2()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        SceneManager.LoadScene("Scene2");
       
    }

    public void Level3()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        SceneManager.LoadScene("Scene3");
    }

    public void Creditos()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        SceneManager.LoadScene("Credits");
    }

    public void Quitar()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        Application.Quit();
    }


    public void Volver()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        SceneManager.LoadScene("MainMenu");
        home.SetActive(true);
        levelSelect.SetActive(false);
    }
}
