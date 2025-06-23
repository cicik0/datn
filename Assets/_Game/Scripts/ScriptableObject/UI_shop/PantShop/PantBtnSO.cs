using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantBtnSO", menuName = "Scriptable Objects/PantBtnSO")]
public class PantBtnSO : ScriptableObject
{
    public List<PantBtnData> pantBtnListSO;
    public ButtonItemPantUI pf_pantBtnUI;
    public EnumItemCategory itemType;
}
