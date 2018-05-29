using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayState
{
    playing,//正在游戏
    stop,//暂停
    over,//结束
}

public class GameManager : MonoBehaviour {

    [HideInInspector]public static GameManager Instance;

    private int score;

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int add)
    {
        score += add;
    }

    public PlayState playState = PlayState.playing;
    public SelectPanel selectPanel;
    public SelectPanel overPanel;
    public GameObject yes_select;
    public GameObject no_select;
    private bool yes = true;

    private void Awake()
    {
        Instance = this;
    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && playState == PlayState.playing)
        {
            selectPanel.Down();
            Time.timeScale = 0;
            playState = PlayState.stop;
        }
        else if (playState == PlayState.stop && Input.GetKeyDown(KeyCode.Y))
        {
            yes = true;
            yes_select.SetActive(true);
            no_select.SetActive(false);
        }
        else if (playState == PlayState.stop && Input.GetKeyDown(KeyCode.N))
        {
            yes = false;
            yes_select.SetActive(false);
            no_select.SetActive(true);
        }
        else if (playState == PlayState.stop && Input.GetKeyDown(KeyCode.Space))
        {
            if (yes == true)
            {
                selectPanel.Up();
                StartCoroutine(begin());
                playState = PlayState.playing;
            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }

        }

        else if (playState == PlayState.over && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }

    //控制结束界面的显示
    public void Over()
    {
        playState = PlayState.over;
        overPanel.Down();
    }
      
    IEnumerator begin()
    {
        yield return new WaitForSeconds(0.6f);
        Time.timeScale = 0;
    }
}
