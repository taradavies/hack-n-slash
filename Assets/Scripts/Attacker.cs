using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    [SerializeField] int _attackDamage = 1;
    [SerializeField] float _attackRefreshSpeed = 1.5f;
    [SerializeField] float _attackOffset = 1f;
    [SerializeField] float _attackRadius = 1f;

    public bool CanAttack => _attackTimer >= _attackRefreshSpeed;
    public int Damage => _attackDamage;
    public Transform Transform => transform;

    Collider[] _attackResults;
    float _attackTimer;

    void Awake()
    {
        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null) 
            animationImpactWatcher.OnImpact += AnimatorImpactWatcher_OnImpact;

        _attackResults = new Collider[10];
    }

    public void Attack(ITakeHit target)
    {
        _attackTimer = 0;
        target.TakeHit(this);
    }

    void Update()
    {
        _attackTimer += Time.deltaTime;
    }

    // called by the animatorimpactwatcher event which is activated by the script animationimpactwatcher
    void AnimatorImpactWatcher_OnImpact()
    {
        Vector3 position = transform.position + transform.forward * _attackOffset;
        // returns what is in the area defined by the sphere
        int hitCount = Physics.OverlapSphereNonAlloc(position, _attackRadius, _attackResults);

        for (int i = 0; i < hitCount; i++)
        {
            var takesHit = _attackResults[i].GetComponent<ITakeHit>();
            if (takesHit != null)
            {
                takesHit.TakeHit(this);
            }
        }
    }

}
