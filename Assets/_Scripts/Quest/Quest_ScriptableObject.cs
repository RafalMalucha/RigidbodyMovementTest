using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest_ScriptableObject", order = 1)]
public class Quest_ScriptableObject : ScriptableObject
{
    [SerializeField] private Quest_State _questState;
    [SerializeField] private string _questUniqueName;

    public string QuestDisplayName;
    public string ObjectiveString;
    public GameObject[] Rewards;
}
