using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField] int _attackDamage = 1;
    [SerializeField] float _attackRefreshSpeed = 1.5f;

    public bool CanAttack => _attackTimer >= _attackRefreshSpeed;
    public int Damage => _attackDamage;
    public Transform Transform => transform;

    float _attackTimer;

    public void Attack(ITakeHit target)
    {
        _attackTimer = 0;
        target.TakeHit(this);
    }

    void Update()
    {
        _attackTimer += Time.deltaTime;
    }

}
