using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Transform pivot;

    private Ctrl ctrl;

    private GameManager gameManager;    

    private bool isPause = false;   //是否暂停

    private bool isSpeedup = false; //是否加速

    private float timer = 0;        //运动时间间隔

    private float stepTime = 0.8f;  //运动一格需要时间

    private int multiple = 6;       //加速为原速度的4倍
    private void Awake()
    {
        pivot = transform.Find("Pivot");
    }
    private void Update()
    {
        if (isPause)
        {
            return;
        }
        timer += Time.deltaTime;
        if(timer > stepTime)
        {
            timer = 0;
            Fall();
        }
        InputControl();
    }

    public void Init(Color color, Ctrl ctrl, GameManager gameManager)
    {
        //遍历孩子
        foreach(Transform t in  transform)
        {
            if(t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        this.ctrl = ctrl;
        this.gameManager = gameManager;
    }

    //方块下落
    public void Fall()
    {
        Vector3 pos = transform.position;
        pos.y -= 1;
        transform.position = pos;

        if (ctrl.model.IsValidMapPosition(this.transform) == false)
        {          
            pos.y += 1;
            transform.position = pos;
            isPause = true;            
            bool isLineClear = ctrl.model.PlaceShape(this.transform);

            //是否有行消除
            if (isLineClear)
            {
                Debug.Log("player");
                ctrl.audioManager.PlayLineClear();
            }
            gameManager.FallDown();
            return;
        }
        ctrl.audioManager.PlayDrop();
    }

    //方向控制
    private void InputControl()
    {
        
        // float h = Input.GetAxisRaw("Horizontal");  //这里是一个未解决的BUG
        float h = 0;
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            h = -1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            h = 1;
        }

        if (h != 0)
        {
            Vector3 pos = transform.position;
            pos.x += h;
            transform.position = pos;
            if(ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                pos.x -= h;
                transform.position = pos;                
            }
            else
            {
                ctrl.audioManager.PlayControl();
                
            }                        
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //旋转
            transform.RotateAround(pivot.position, Vector3.forward, -90);
            if (ctrl.model.IsValidMapPosition(this.transform) == false)
            {
                transform.RotateAround(pivot.position, Vector3.forward, 90);
            }
            else
            {
                ctrl.audioManager.PlayControl();
             
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            isSpeedup = true;
            stepTime /= multiple;
        }
    }

    //暂停方块下落
    public void Pasue()
    {
        isPause = true;
    }

    //方块继续下落
    public void Resume()
    {
        isPause = false;
    }
}
