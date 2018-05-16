using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏状态
public class PlayState : FSMState
{
    //初始游戏状态
    private void Awake()
    {
        stateID = StateID.Play;
        AddTransition(Transition.PauseButtonClick, StateID.Menu);
    }

    public override void DoBeforeEntering()
    {
        ctrl.view.ShowGameUI(ctrl.model.Score,ctrl.model.HigScore);
        ctrl.cameraManager.ZoomIn();
        ctrl.gameManager.StartGame();
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideGameUI();
        ctrl.view.ShowRestartButton();
        ctrl.gameManager.PauseGame();
    }

    //游戏暂停
    public void OnPauseButtonClick()
    {
        ctrl.audioManager.PlayCursor();
        fsm.PerformTransition(Transition.PauseButtonClick);
    }

    //重新开始
    public void OnRestartButtonClick()
    {
        ctrl.view.HideGameOverUI();
        ctrl.model.Restart();
        ctrl.gameManager.StartGame();
        ctrl.view.UpdateGameUI(0, ctrl.model.HigScore);
    }


}
