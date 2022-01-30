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
        //SceneManager.LoadScene("SampleScene");
        home.SetActive(false);
        levelSelect.SetActive(true);
    }


    public void Creditos()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Quitar()
    {
        Application.Quit();
    }

}
