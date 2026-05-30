using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Mode Database")]
public class ModeDatabaseSO : ScriptableObject
{
    public List<ModeDataSO> modes;
}
