using UnityEngine;

public interface IButtle
{
    void AddDamage(int damage, GameObject gameObject);
    void KnockBack(Transform transform, float powor);
    int HP();
    int MAX_HP();

}