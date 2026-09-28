using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests/Quest_ScriptableObject", order = 1)]
public class Quest_ScriptableObject : ScriptableObject
{
    [SerializeField] private Quest_State QuestState;

    public string QuestName;
    public string ObjectiveString;
    public GameObject[] Rewards;
}
