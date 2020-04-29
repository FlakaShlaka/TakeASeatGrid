using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{

    [SerializeField] float timeLeft;
    public Text timerText;
    
    public GameObject endGameScreen;
    public GameObject winScreen;

    [SerializeField] float spawnTime;
    public Text spawnText;


    public bool hasStarted = false;
    public bool hasFinished = false;

    private bool clock;
   
    void Update()
    {
        return; // TODO: Add canvas
        if (!hasFinished)
        {
            if (timeLeft > 0 && clock == false)
            {
                clock = true;
                StartCoroutine(WaitforTimer());
                if (hasStarted)
                {
                    StartCoroutine(WaitforSpawn());
                }
            }
            else if (timeLeft == 0)
            {
                Time.timeScale = 0f;
                endGameScreen.SetActive(true);
            }

        }
        else if (hasFinished)
        {
            winScreen.gameObject.SetActive(true);
        }

    }
    IEnumerator WaitforTimer()
    {

        timeLeft -= 1;
        UpdateTimer();
        yield return new WaitForSeconds(1);

        clock = false;
    }

    IEnumerator WaitforSpawn()
    {
        spawnTime -= 1;
        if (spawnTime > 0)
        {
            int spawnSec = Mathf.FloorToInt(spawnTime % 60);
            spawnText.GetComponent<UnityEngine.UI.Text>().text = /*min.ToString("0") + ":" +*/ spawnSec.ToString("00");
        }
        else if (spawnTime == 0)
        {
            int spawnSec = Mathf.FloorToInt(spawnTime % 60);
            spawnTime = 4;
            spawnText.GetComponent<UnityEngine.UI.Text>().text = /*min.ToString("0") + ":" +*/ spawnSec.ToString("00");
        }

        yield return new WaitForSeconds(1);
    }


    void UpdateTimer()
    {

        int min = Mathf.FloorToInt(timeLeft / 60);
        int sec = Mathf.FloorToInt(timeLeft % 60);
        timerText.GetComponent<UnityEngine.UI.Text>().text = min.ToString("0") + ":" + sec.ToString("00");
    }
}