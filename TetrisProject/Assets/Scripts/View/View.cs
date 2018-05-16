using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//UI视图
public class View : MonoBehaviour
{
    private Ctrl ctrl;

    //UI控件
    private RectTransform logoName;
    private RectTransform menuUI;
    private RectTransform gameUI;
    private GameObject restartButton;
    private GameObject gameOverUI;
    private GameObject settingUI;
    private GameObject rankUI;

    private GameObject mute;

    private Text score;
    private Text highScore;
    private Text gameOverScore;

    //排名UI下的score等
    private Text rankScore;
    private Text rankHighScore;
    private Text rankNumbersGame;


    // Use this for initialization
    void Awake ()
    {
        ctrl = GameObject.FindGameObjectWithTag("Ctrl").GetComponent<Ctrl>();

        logoName = transform.Find("Canvas/LogoName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI") as RectTransform;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        settingUI = transform.Find("Canvas/SettingUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;
        restartButton = transform.Find("Canvas/MenuUI/RestartButton").gameObject;

        score = transform.Find("Canvas/GameUI/ScoreLabel/text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScoreLabel/text").GetComponent<Text>();
        gameOverScore  = transform.Find("Canvas/GameOverUI/score").GetComponent<Text>();
        //Debug.Log(gameOverScore + "gameover");

        mute = transform.Find("Canvas/SettingUI/AudioButton/mute").gameObject;

        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/score").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighScoreLabel/score").GetComponent<Text>();
        rankNumbersGame = transform.Find("Canvas/RankUI/NumberGamesLabel/text").GetComponent<Text>();
    }
	
    //显示Logo及其菜单栏
	public void ShowMenu()
    {
        //启用logoname设置目标位置
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-173.5f, 0.5f);

        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(62f, 0.5f);
    }

    //隐藏Logo和菜单栏
    public void HideMenu()
    {
        logoName.DOAnchorPosY(173.5f, 0.5f)
            .OnComplete(delegate { logoName.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-62f, 0.5f)
            .OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }

    //显示游戏界面
    public void ShowGameUI(int score = 0, int highScore = 0)
    {

        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-96f, 0.5f);
    }

    //隐藏游戏界面
    public void HideGameUI()
    {
        gameUI.DOAnchorPosY(121.5f, 0.5f)
            .OnComplete(delegate { gameUI.gameObject.SetActive(false); });
    }

    //更新分数UI
    public void UpdateGameUI(int score, int highScore)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
    }

    //重置
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
    }

    //显示游戏结束UI
    public void ShowGameOverUI(int score = 0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = score.ToString();
    }

    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }

    //回到主页
    public void OnHomeButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        //加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //设置按钮
    public void OnSettingButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(true);
    }

    //是否静音的显示
    public void SetMuteActive(bool isActive)
    {
        mute.SetActive(isActive);
    }

    //打开知乎主页
    public void OnZhihuButtonClick()
    {        
        Application.OpenURL("https://www.zhihu.com/people/xiao-y-12-21/activities");        
    }

    //打开github主页
    public void OnGitButtonClick()
    {
        Application.OpenURL("https://github.com/Z-bin"); 
    }

    //隐藏设置界面
    public void OnSettingUIClick()
    {
        ctrl.audioManager.PlayCursor();
        settingUI.SetActive(false);
    }

    //排名分数UI显示
    public void ShowRankUI(int score,int highScore, int numbersGame)
    {        
        this.rankScore.text = score.ToString();
        this.rankHighScore.text = highScore.ToString();
        this.rankNumbersGame.text = numbersGame.ToString();
        rankUI.SetActive(true);
    }

    //隐藏RankUI
    public void OnRankUIClick()
    {
        rankUI.SetActive(false);
    }
}
