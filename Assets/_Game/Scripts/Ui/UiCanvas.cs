using UnityEngine;

public class UiCanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClosed;

    protected RectTransform m_rectTranform;
    private Animator m_animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //khoi tao gia tri canvas
    protected void OnInit()
    {

    }

    public virtual void SetUp()
    {

    }

    //cai nay k bt la gi, tim hieu sau (danh cho android)
    public void Backey()
    {

    }

    //mo canva
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    //dong canvas ngay lap tuc
    public virtual void CloseDirectly()
    {
        gameObject.SetActive(false);
        if (isDestroyOnClosed)
        {
            Destroy(gameObject);
        }
    }

    //dong canvas sau mot khoang thoi gian
    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

    //
}
