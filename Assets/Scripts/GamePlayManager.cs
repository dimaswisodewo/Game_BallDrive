using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public Text levelText, timerText, highscoreText, berhasilText;
    private float timer, getMinutes, getSeconds, getMiliSeconds = 0f;
    private Scene level;
    private bool finished = false;
    public Transform player;

    private void Start()
    {
        berhasilText.enabled = false;
        level = SceneManager.GetActiveScene();
        levelText.text = level.name;
        highscoreText.text = "HighScore = " + PlayerPrefs.GetFloat("HSMinutes", 0).ToString("F0") + " : "
                             + PlayerPrefs.GetFloat("HSSeconds", 0).ToString("F0") + " : "
                             + PlayerPrefs.GetFloat("HSMiliSec", 0).ToString("F0");
    }

    private void Update()
    {
        Timer();
        LoseReset();
        QuitToMenu();
        if (finished)
        {
            Debug.Log("Finished");
            if(level.name == "LEVEL 2")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SceneManager.LoadScene("MENU");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(level.buildIndex + 1);
            }
        }
    }

    private void Timer()
    {
        timer += Time.deltaTime;
        getMinutes = timer / 60;
        getSeconds = timer % 60;
        getMiliSeconds = (timer % 1) * 100;
        timerText.text = getMinutes.ToString("F0") + " : " +
                         getSeconds.ToString("F0") + " : " +
                         getMiliSeconds.ToString("F0");
    }

    public void LoseReset()
    {
        if (player.position.y <= -8)
        {
            timer = 0f;
            SceneManager.LoadScene(level.name);
        }
    }

    private void Finish()
    {
        CekHighScore();
        berhasilText.text = "Berhasil Menyelesaikan Dengan Waktu " + getMinutes.ToString("F0") + " : " +
                            getSeconds.ToString("F0") + " : " +
                            getMiliSeconds.ToString("F0") +
                            ", Waktu Tercepat Saat Ini " + PlayerPrefs.GetFloat("HSMinutes", 0).ToString("F0") + " : "
                            + PlayerPrefs.GetFloat("HSSeconds", 0).ToString("F0") + " : "
                            + PlayerPrefs.GetFloat("HSMiliSec", 0).ToString("F0")
                            + "(Tekan Enter Untuk Lanjut)";
        highscoreText.text = "HighScore = " + PlayerPrefs.GetFloat("HSMinutes", 0).ToString("F0") + " : "
                             + PlayerPrefs.GetFloat("HSSeconds", 0).ToString("F0") + " : "
                             + PlayerPrefs.GetFloat("HSMiliSec", 0).ToString("F0");
        berhasilText.enabled = true;
        finished = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Finish();
        }
    }

    private void QuitToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MENU");
        }
    }

    private void CekHighScore()
    {
        if (PlayerPrefs.GetFloat("HSMinutes", 0) == 0 && PlayerPrefs.GetFloat("HSSeconds", 0) == 0 && PlayerPrefs.GetFloat("HSMiliSec", 0) == 0)
        {
            PlayerPrefs.SetFloat("HSMinutes", getMinutes);
            PlayerPrefs.SetFloat("HSSeconds", getSeconds);
            PlayerPrefs.SetFloat("HSMiliSec", getMiliSeconds);
        }
        else if (getMinutes < PlayerPrefs.GetFloat("HSMinutes", 0))
        {
            PlayerPrefs.SetFloat("HSMinutes", getMinutes);
            PlayerPrefs.SetFloat("HSSeconds", getSeconds);
            PlayerPrefs.SetFloat("HSMiliSec", getMiliSeconds);
        }
        else if (getMinutes == PlayerPrefs.GetFloat("HSMinutes", 0))
        {
            if (getSeconds < PlayerPrefs.GetFloat("HSSeconds", 0))
            {
                PlayerPrefs.SetFloat("HSSeconds", getSeconds);
                PlayerPrefs.SetFloat("HSMiliSec", getMiliSeconds);
            }
            else if (getSeconds == PlayerPrefs.GetFloat("HSSeconds", 0))
            {
                if (getMiliSeconds < PlayerPrefs.GetFloat("HSMiliSec", 0))
                {
                    PlayerPrefs.SetFloat("HSMiliSec", getMiliSeconds);
                }
            }
        }
    }

}
