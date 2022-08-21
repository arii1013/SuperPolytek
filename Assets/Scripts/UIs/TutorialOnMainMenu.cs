using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialOnMainMenu : MonoBehaviour
{
    public void TutorialStart()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
