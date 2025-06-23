using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] FloatingJoystick floatingJoystick;
    [SerializeField] bool CanMove;
    [SerializeField] bool isModel;

    public Action OnPlayerDead;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        //ChangeAnim(Constant.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Throw();
        //}
        Move();
    }

    public override void OnInit()
    {
        base.OnInit();
        if(isModel)
        charHat.DesTroyHatView();
        //CanMove = true;
    }

    private void Move()
    {
        if (!CanMove) return;

        rb.linearVelocity = new Vector3(floatingJoystick.Horizontal * speed, rb.linearVelocity.y, floatingJoystick.Vertical * speed);
        if(floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
            ChangeAnim(Constant.RUN);
            //ChangeAnim("");
        }
        if(floatingJoystick.Horizontal == 0 && floatingJoystick.Vertical == 0 && isDead == false)
        {
            if(targetTranform != null)
            {
                Throw();
            }
            else
            {
                ChangeAnim(Constant.IDLE);
            }
        }
    }

    public void SetJoystick(FloatingJoystick jt)
    {
        floatingJoystick = jt;
    }

    [ContextMenu("Player die")]
    public override void OnDead()
    {
        base.OnDead();

        OnPlayerDead?.Invoke();
    }
}
