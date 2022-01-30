using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Iniciar()
    {
        SoundManager.Instance.PlaySound(SoundNames.ingameButton);
        SceneManager.LoadScene("SampleScene");
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
