using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionComponent : MonoBehaviour
{
    [SerializeField] Collider2D _col;
    [SerializeField] protected bool _pushCollidingObject;
    [SerializeField] protected bool _damageCollidingObject;
    [SerializeField] protected int damage;

    protected PushInfo _pushInfo;
    protected DamageInfo _damageInfo;

    private void Start()
    {
        _pushInfo = new PushInfo( transform.position,new Collider2D[] { _col });
        _damageInfo = new DamageInfo(damage,transform.position,new Collider2D[] { _col });
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (_pushCollidingObject)
        {
            IPushable toPush = collision.transform.GetComponentInParent<IPushable>();
            if (toPush != null)
            {
                _pushInfo.pushPosition = transform.position;
                toPush.Push(_pushInfo);
            }
        }
        if (_damageCollidingObject)
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
