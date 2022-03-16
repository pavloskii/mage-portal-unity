using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IdleState : StateMachineBehaviour
{
    public const string MOVE_STATE = "Move";
    const string FIRE_STATE = "Fire";

    WidgetController m_widgetController;
    EnemyManager m_enemyManager;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_widgetController == null)
        {
            m_widgetController = WidgetController.GetInstance();
        }

        if (m_enemyManager == null)
        {
            m_enemyManager = EnemyManager.GetInstance();
        }

        animator.ResetTrigger(MOVE_STATE);
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_widgetController.GetDirection() != Vector3.zero) { animator.SetTrigger(MOVE_STATE); }
        else if (m_enemyManager.TargetExists())animator.SetTrigger(FIRE_STATE);
    }
}









