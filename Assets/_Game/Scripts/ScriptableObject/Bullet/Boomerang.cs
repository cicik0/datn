using System.Collections;
using UnityEngine;

public class Boomerang : Bullet
{
    [SerializeField] float rotateSpeed;
    protected override void EffectThrow()
    {
        //Debug.Log("boomerang");   
        this.bulletView.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }




}
