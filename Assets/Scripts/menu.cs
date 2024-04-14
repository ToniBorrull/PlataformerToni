using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject credits;
    public Scene mainmenu;
    public Animator transition;
    public AudioSource audio;
    

    public float transitionTime = 1f;

    void Update()
    {

    }
    public void Play()
    {
        audio.Play(0);
        LoadNextLevel(1);
        
    }
    public void ReturnMenu()
    {
        LoadNextLevel(-1);
    }
   
    public void Credits()
    {
        audio.Play(0);
        menu.SetActive(false);
        credits.SetActive(true);
    }
   
    public void Return2()
    {
        credits.SetActive(false);
        menu.SetActive(true);
        audio.Play(0);
        
    }

    public void Quit()
    {
        audio.Play(0);
        Debug.Log("He salido de la aplicacion");
        Application.Quit();
    }

    public void LoadNextLevel(int index)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + index));
    }

    IEnumerator LoadLevel(int index)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
