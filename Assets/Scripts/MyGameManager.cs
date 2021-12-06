using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvasMain;
    [SerializeField] private GameObject canvasGameOver;
    [SerializeField] private GameObject canvasWin;
    public enum GameStates
    {
        Playing,
        GameOver,
        Win
    }
    public GameStates gameState = GameStates.Playing;

    //private Health healthPlayer;
    //public Text textHealth;
    //public string healthPrefix = "Health: ";

    private int score;
    [SerializeField] private Text textScore;
    [SerializeField] private string scorePrefix = "Score: ";

    private PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        //healthPlayer = player.GetComponent<Health>();
        playerControl = player.GetComponent<PlayerControl>();

        canvasMain.SetActive(true);
        canvasGameOver.SetActive(false);

        score = 0;
        textScore.text = scorePrefix + score;
        //textHealth.text = healthPrefix + healthPlayer.healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        //// Game State Machine
        //switch (gameState)
        //{
        //    case GameStates.Playing:
        //        if (!healthPlayer.isAlive)
        //        {
        //            gameState = GameStates.GameOver;
        //            canvasMain.SetActive(false);
        //            canvasGameOver.SetActive(true);
        //            Debug.Log("Set GameOver");
        //        }
        //        if (playerControl.win)
        //        {
        //            gameState = GameStates.Win;
        //            canvasMain.SetActive(false);
        //            canvasWin.SetActive(true);
        //            Debug.Log("Set Win canvas");
        //        }
        //        break;
        //    case GameStates.GameOver:
        //        break;
        //    case GameStates.Win:
        //        break;
        //}

        //textHealth.text = healthPrefix + healthPlayer.healthPoints;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        textScore.text = scorePrefix + score;
    }
}
