using UnityEngine;

public class Player : Character
{
    [SerializeField] FloatingJoystick floatingJoystick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        //ChangeAnim(Constant.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Throw();
        }
        Move();
    }

    public override void OnInit()
    {
        base.OnInit();
    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(floatingJoystick.Horizontal * speed, rb.linearVelocity.y, floatingJoystick.Vertical * speed);
        if(floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
            //ChangeAnim(Constant.RUN);
            ChangeAnim("IsRun");
        }
        if(floatingJoystick.Horizontal == 0 && floatingJoystick.Vertical == 0)
        {
            ChangeAnim(Constant.IDLE);
        }
    }

    private void GetJoystick()
    {

    }
}
