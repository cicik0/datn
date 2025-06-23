using UnityEngine;

public class PatrolState : IsState
{
    public void OnEnter(Enemy enemy)
    {
        //Debug.LogError("PATROL STATE");
        enemy.agent.isStopped = false;
        enemy.agent.SetDestination(enemy.GetRandomPointInNavmesh(LevelManager.Ins.GetPlayerPosition(), enemy.radius));
        enemy.ChangeAnim(Constant.RUN);
    }

    public void OnExcute(Enemy enemy)
    {
        if(enemy.targetTranform != null && enemy.isDead == false)
        {
            enemy.ChangeState(new WatchState());
            return;
        }

        if (!enemy.agent.pathPending && enemy.agent.remainingDistance <= enemy.agent.stoppingDistance)
        {
            enemy.ChangeState(new WatchState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
