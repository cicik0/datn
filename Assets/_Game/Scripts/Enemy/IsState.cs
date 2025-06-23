using UnityEngine;

public interface IsState
{
    public void OnEnter(Enemy enemy);
    public void OnExcute(Enemy enemy);
    public void OnExit(Enemy enemy);
}
