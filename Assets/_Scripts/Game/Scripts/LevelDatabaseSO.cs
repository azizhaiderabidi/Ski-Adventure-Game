using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Database")]
public class LevelDatabaseSO : ScriptableObject
{
    public List<LevelDataSO> levels;



    [Button]
    void SetAllIndexes()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].levelIndex = i;
        }
    }
}

