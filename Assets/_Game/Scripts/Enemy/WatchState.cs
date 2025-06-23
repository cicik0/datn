using UnityEngine;

public class WatchState : IsState
{
    private float timer;

    public void OnEnter(Enemy enemy)
    {
        enemy.agent.isStopped = true;
        enemy.agent.velocity = Vector3.zero;
        enemy.ChangeAnim(Constant.IDLE);
        timer = enemy.RandomWatchTime();
        //Debug.LogError($"WATCH STATE {timer}");
    }

    public void OnExcute(Enemy enemy)
    {
        timer -= Time.deltaTime;

        if (!enemy.isDead && enemy.targetTranform != null)
        {
            enemy.ChangeState(new AttackState());
        }

        if (timer <= 0)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        //timer = 0;
    }
}
