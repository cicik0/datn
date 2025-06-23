using UnityEngine;

public class AttackState : IsState
{
    float timer;
    float attackTime = 0.5f;
    public void OnEnter(Enemy enemy)
    {
        //Debug.LogError("ATTACK STATE");
        enemy.agent.isStopped = true;
        enemy.agent.ResetPath();

        timer = attackTime;
        enemy.Attack();
    }

    public void OnExcute(Enemy enemy)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            enemy.ChangeState(new WatchState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
