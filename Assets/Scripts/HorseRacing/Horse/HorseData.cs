using UnityEngine;

[CreateAssetMenu(fileName = "HorseData", menuName = "Scriptable Objects/HorseData")]
public class HorseData : ScriptableObject
{
    public string horseName;
    public float speed;
    public string story;
    // TODO: 말 스킬 추가

    public Sprite portrait; // 말 초상화 이미지
    public GameObject model; // 실제로 움직이는 말 오브젝트
}
