using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HatBtnSO", menuName = "Scriptable Objects/HatBtnSO")]
public class HatBtnSO : ScriptableObject
{
    public List<HatBtnData> hatBtnListSO;
    public ButtonItemHatUI pf_hatBtnUI;
    public EnumItemCategory itemType;
}
