using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInteractionComponent : MonoBehaviour
{
    [SerializeField] LayerMask _exludeFromHitting;
    [SerializeField] protected bool _pushCollidingObject;
    [SerializeField] protected bool _damageCollidingObject;
    [SerializeField] protected int _damage;

    protected PushInfo _pushInfo;
    protected DamageInfo _damageInfo;


    public Vector3 Position { get => transform.position; }

    private void Start()
    {
        _pushInfo = new PushInfo( transform.position);
        _damageInfo = new DamageInfo(_damage,transform.position);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_exludeFromHitting != (_exludeFromHitting | (1 << collision.otherCollider.gameObject.layer))) return;
        if (_pushCollidingObject)
        {
            IPushable toPush = collision.transform.GetComponentInParent<IPushable>();
            if (toPush != null)
            {
                _pushInfo.pushPosition = transform.position;
                toPush.Push(_pushInfo);
            }
        }
        if(_damageCollidingObject)
        {
            IDamagable toDamage = collision.transform.GetComponentInParent<IDamagable>();
            if (toDamage != null)
            {
                _damageInfo.dmgPosition = transform.position;
                toDamage.TakeDamage(_damageInfo);
            }
        }
    }
}
