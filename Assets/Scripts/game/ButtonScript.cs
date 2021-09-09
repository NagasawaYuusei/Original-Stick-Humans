using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    GameObject setting;

    [SerializeField] AudioClip sound1;
    [SerializeField] AudioClip sound2;
    AudioSource audioSource;
    public void ButtonClassSetting()
    {
        setting = GameObject.Find("Player States");
        Destroy(setting);
        
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        Time.timeScale = 1f;
        Invoke("Setting", 0.5f);
   
    }

    public void ButtonClassOperating()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        Invoke("Operating", 0.6f);
    }

    public void ButtonClassStart()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound2);
        Invoke("Game", 2);
    }

    public void ButtonClassTitle()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound1);
        Time.timeScale = 1f;
        Invoke("Star", 0.5f);
       
    }
    public void end()
    {
        Application.Quit();
    }
    void Setting()
    {
        SceneManager.LoadScene("Setting");
    }

    void Operating()
    {
        SceneManager.LoadScene("Operating");
    }

    void Game()
    {
        SceneManager.LoadScene("Game");
    }

    void Star()
    {
        SceneManager.LoadScene("Start");
    }




}