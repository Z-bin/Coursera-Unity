using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool isPause = true; //游戏是否暂停

    private Shape currentShape = null;

    private Ctrl ctrl;

    private Transform blockHolder; //block的父亲

    public Shape[] shapes;      //存放图形

    public Color[] colors;      //图形颜色

    private void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        blockHolder = transform.Find("BlockHolder");
    }
    
	
	// Update is called once per frame
	void Update ()
    {
	    if(isPause)
        {
        //    Debug.Log("pause");
            return;
        }
        if(currentShape == null)
        {
         //   Debug.Log("yes");
            SpawmShape();
        }
	}

    //重新开始游戏时清除当前方块
    public void ClearShape()
    {
        if(currentShape!=null)
        {
            Destroy(currentShape.gameObject);
        }
    }


    public void StartGame()
    {
     //   Debug.Log("startgame");
        isPause = false;
        if(currentShape != null)
        {
            currentShape.Resume();
        }
    }

    public void PauseGame()
    {
        isPause = true;
        if(currentShape != null)
        {
            currentShape.Pasue();
        }
    }

    //生成图形
    void SpawmShape()
    {
        int index = Random.Range(0, shapes.Length);         //随机一个索引得到一个shape
        int indexColor = Random.Range(0, colors.Length);    //随机颜色
        currentShape = GameObject.Instantiate(shapes[index]);
        currentShape.transform.position = new Vector3(4, 20, 0);
        currentShape.transform.parent = blockHolder;
        currentShape.Init(colors[indexColor],ctrl,this);
    }

    //方块已经落下
    public void FallDown()
    {
        currentShape = null;        
        //分数及最高分数是否更新
        if(ctrl.model.isDataUpdate)
        {
            ctrl.view.UpdateGameUI(ctrl.model.Score, ctrl.model.HigScore);
        }

        //清除没有方块的物体
        foreach(Transform t in blockHolder)
        {
            if(t.childCount <= 1)
            {
                Destroy(t.gameObject);
            }
        }
        //游戏结束就暂停游戏
        if(ctrl.model.IsGameOver())
        {
            PauseGame();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
        }
    }
}
