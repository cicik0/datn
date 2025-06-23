using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class UiManager : Singleton<UiManager>
{
    //list load ui tu resources
    [SerializeField] private UiCanvas[] uiResources;
 
    //luu prefabs tam thoi de truy cap nhanh hon
    private Dictionary<System.Type, UiCanvas> uiCanvasPrefabs = new Dictionary<System.Type, UiCanvas>();

    //luu cac ui dang dung
    private Dictionary<System.Type, UiCanvas> uiCanvas = new Dictionary<System.Type, UiCanvas>();

    [SerializeField] public Transform CanvasParentTF;

    private void OnEnable()
    {
        if(uiResources == null || uiResources.Length == 0)
        {
            uiResources = Resources.LoadAll<UiCanvas>("UI/");
        }
    }

    #region Canvas

    //mo ui
    public T OpenUI<T>() where T : UiCanvas
    {
        UiCanvas canvas = GetUI<T>();

        canvas.SetUp();
        canvas.Open();

        return canvas as T;
    }

    //dong ui ngay lap tuc
    public void CloseUI<T>() where T : UiCanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().CloseDirectly();
        }
    }

    //dong ui sau delay time
    public void CloseUI<T>(float delayTime) where T : UiCanvas
    {
        if (IsOpened<T>())
        {
            GetUI<T>().Close(delayTime);
        }
    }

    //kiem tra ui da dc mo len man hinh hay chua
    public bool IsOpened<T>() where T : UiCanvas
    {
        return IsLoaded<T>() && uiCanvas[typeof(T)].gameObject.activeInHierarchy;
    }

    //kiem tra ui khoi tao hay chua
    public bool IsLoaded<T>() where T : UiCanvas
    {
        System.Type type = typeof(T);
        return uiCanvas.ContainsKey(type) && uiCanvas[type] != null;
    }

    //lay compoment ui hien tai
    public T GetUI<T>() where T : UiCanvas
    {
        if (!IsLoaded<T>())
        {
            UiCanvas canvas = Instantiate(GetUIPrefab<T>(), CanvasParentTF);
            uiCanvas[typeof(T)] = canvas;
        }

        return uiCanvas[typeof(T)] as T;
    }

    //dong toan bo ui dang mo ngay lap tuc
    public void CloseAll()
    {
        foreach(var item in uiCanvas)
        {
            if(item.Value != null && item.Value.gameObject.activeInHierarchy)
            {
                item.Value.CloseDirectly();
            }
        }
    }

    //lay canvas prefab tu Resources/UI
    private T GetUIPrefab<T>() where T : UiCanvas
    {
        if (!uiCanvasPrefabs.ContainsKey(typeof(T)))
        {
            for(int i = 0; i < uiResources.Length; i++)
            {
                if (uiResources[i] is T)
                {
                    uiCanvasPrefabs[typeof(T)] = uiResources[i];
                    break;
                }
            }
        }
        return uiCanvasPrefabs[typeof(T)] as T;
    }

    #endregion


    #region Back button

    //luu ui va action khi nhan nut back
    private Dictionary<UiCanvas, UnityAction> BackActionEvents = new Dictionary<UiCanvas, UnityAction>();

    //luu cac ui da mo duoi dang stack
    private List<UiCanvas> backCanvas = new List<UiCanvas>();

    //lay ui xuat hien tren cung (ui cuoi cua backCanvas). ui nay se su ly backEvent
    private UiCanvas BackTopUI
    {
        get
        {
            UiCanvas canvas = null;
            if(backCanvas.Count > 0)
            {
                canvas = backCanvas[backCanvas.Count - 1];
            }
            return canvas;
        }
    }

    //moi frame, neu an ESC, kiem tra co ui mo tren cung hay khong (BackTopUI), neu co goi UnityAction tuong ung
    private void LateUpdate()
    {
        if(Input.GetKey(KeyCode.Escape) && BackTopUI != null)
        {
            BackActionEvents[BackTopUI]?.Invoke();
        }
    }

    //han ham back cho mot ui cu the
    public void PushBackAction(UiCanvas canvs, UnityAction action)
    {
        if (!BackActionEvents.ContainsKey(canvs))
        {
            BackActionEvents.Add(canvs, action);
        }
    }

    //them ui vao danh sach back
    public void AddBackUI(UiCanvas canvas)
    {
        if (!backCanvas.Contains(canvas))
        {
            backCanvas.Add(canvas);
        }
    }

    //go cac ui trong danh sach back
    public void RemoveBackUI(UiCanvas canvas)
    {
        backCanvas.Remove(canvas);
    }

    //don stack back, dung khi reset ui hoac ve man hinh chinh
    public void ClearBackKey()
    {
        backCanvas.Clear();
    }

    #endregion
}
