using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//菜单状态
public class MenuState : FSMState
{
    //初始游戏状态
    private void Awake()
    {
        stateID = StateID.Menu;
        //游戏状态转换
        AddTransition(Transition.StarButtonClick, StateID.Play);
    }

    public override void DoBeforeEntering()
    {
        ctrl.view.ShowMenu();
        ctrl.cameraManager.ZoomOut();
    }
    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenu();
    }

    //开始游戏
    public void OnStartButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        fsm.PerformTransition(Transition.StarButtonClick);
    }

    //显示排名分数UI
    public void OnRankButtonClick()
    {
        ctrl.view.ShowRankUI(ctrl.model.Score,ctrl.model.HigScore,ctrl.model.NumbersGame);
    }

    //清空数据
    public void OnDestroyButtonClick()
    {
        ctrl.model.ClearData();
        OnRankButtonClick();
    }

    //重新开始游戏
    public void OnRestartButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        ctrl.model.Restart();
        ctrl.gameManager.ClearShape();
        fsm.PerformTransition(Transition.StarButtonClick);
    }
}
