using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;

    public Transform[] room;
    public Transform[] spawnPos;

    public int currentCheckPoint = 0;
    GameObject playerPrefab;
    GameObject player;

    [HideInInspector] public Button btnStart;
    [HideInInspector] public Button btnHowto;
    [HideInInspector] public Button btnQuit;
    [HideInInspector] public GameObject panelTitle;
    [HideInInspector] public GameObject panelHowto;
    [HideInInspector] public Button btnCloseHowto;
    [HideInInspector] public GameObject panelDie;
    [HideInInspector] public GameObject panelWin;
    [HideInInspector] public Button btnTitle;
    [HideInInspector] public Button btnExit;

    public enum GameState
    {
        Title,
        Play,
        Die,
        Win
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btnStart.onClick.AddListener(OnBtnStartClicked);
        btnHowto.onClick.AddListener(OnBtnHowtoClicked);
        btnQuit.onClick.AddListener(OnBtnQuitClicked);
        btnCloseHowto.onClick.AddListener(OnBtnCloseHowtoClicked);
        btnTitle.onClick.AddListener(OnBtnTitleClicked);
        btnExit.onClick.AddListener(OnBtnQuitClicked);

        playerPrefab = (GameObject)Resources.Load("Player");
        panelTitle.SetActive(true);
        panelHowto.SetActive(false);
        panelDie.SetActive(false);
        panelWin.SetActive(false);
        state = GameState.Title;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && state == GameState.Die)
        {
            Respawn();
        }
        
    }

    #region Button
    void OnBtnStartClicked()
    {
        panelTitle.SetActive(false);
        GameStart();
    }

    void OnBtnHowtoClicked()
    {
        panelHowto.SetActive(true);
    }

    void OnBtnQuitClicked()
    {
        Application.Quit();
    }

    void OnBtnCloseHowtoClicked()
    {
        panelHowto.SetActive(false);
    }

    void OnBtnTitleClicked()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    void GameStart()
    {
        state = GameState.Play;
        Respawn();
    }

    public void Die()
    {
        Destroy(player);
        state = GameState.Die;
        panelDie.SetActive(true);
    }

    void Respawn()
    {
        panelDie.SetActive(false);
        player = Instantiate(playerPrefab, spawnPos[currentCheckPoint].position, Quaternion.identity);
        state = GameState.Play;
    }

    public void CameraMove()
    {
        Camera.main.transform.position = new Vector3(room[currentCheckPoint].position.x, room[currentCheckPoint].position.y, -10);
    }

    public void Win()
    {
        state = GameState.Win;
        panelWin.SetActive(true);
        player.SetActive(false);
    }
}
