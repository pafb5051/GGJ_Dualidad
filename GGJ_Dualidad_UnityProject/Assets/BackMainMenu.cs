using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMainMenu : MonoBehaviour
{
    public void Volver()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
}