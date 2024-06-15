using UnityEngine;
[CreateAssetMenu(fileName = "MonsterData", menuName = "ScripttableObject/MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField] private float bulletDamage = default;
    public float BulletDamage { get { return bulletDamage; } }
    [SerializeField] private float bodyDamage = default;
    public float BodyDamage { get { return bodyDamage; } }
    [SerializeField] private float maxHp = default;
    public float MaxHp { get { return maxHp; } }
}
