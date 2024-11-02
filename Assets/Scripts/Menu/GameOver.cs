using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameObject GameOverMal;
    private GameObject GameOverFeli;
    private void Awake() 
    {
        GameOverMal = transform.GetChild(0).gameObject;
        GameOverFeli = transform.GetChild(1).gameObject;
    }
    public void GameRestart() 
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void FinalMalo()
    {
        GameOverMal.SetActive(true);
    }
    public void FinalBueno()
    {
        GameOverFeli.SetActive(true);
    }
}
