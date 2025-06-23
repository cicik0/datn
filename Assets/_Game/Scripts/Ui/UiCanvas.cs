using TMPro;
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

    //set up mac dinh de k bi nha'y hinh
    public virtual void SetUp()
    {
        UiManager.Ins.AddBackUI(this);
        UiManager.Ins.PushBackAction(this, () => BackKey());
    }

    //action back cho android
    public virtual void BackKey()
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

    public virtual void SetText(TMP_Text tmpText, string text)
    {
        tmpText.text = text;
    }

    //dong canvas sau mot khoang thoi gian
    public virtual void Close(float delayTime)
    {
        Invoke(nameof(CloseDirectly), delayTime);
    }

    #region popup
    [Header("Popup child")]
    [SerializeField] UiCanvas[] popups;
    public UiCanvas ParentPopup { get; set; }

    public T GetPopup<T>() where T : UiCanvas
    {
        T ui = null;
        for (int i = 0; i < popups.Length; i++)
        {
            if (popups[i] is T)
            {
                ui = popups[i] as T;
                break;
            }
        }
        return ui;
    }

    public T OpenPopup<T>() where T : UiCanvas
    {
        T ui = GetPopup<T>();
        ui.SetUp();
        ui.Open();
        return ui;
    }

    public bool IsOpenPopup<T>() where T : UiCanvas
    {
        return GetPopup<T>().gameObject.activeSelf;
    }

    public void ClosePopup<T>(float delayTime) where T : UiCanvas
    {
        GetPopup<T>().Close(delayTime);
    }

    public void ClosePopupDirect<T>() where T : UiCanvas
    {
        GetPopup<T>().CloseDirectly();
    }

    public void CloseAllPopup<T>() where T : UiCanvas
    {
        for(int i = 0; i < popups.Length; i++)
        {
            popups[i].CloseDirectly();
        }
    }

    #endregion
}
