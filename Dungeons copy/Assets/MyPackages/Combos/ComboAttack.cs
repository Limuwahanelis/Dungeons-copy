using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ComboAttack")]
public class ComboAttack : ScriptableObject
{
    public float AttackWindowStart => _nextAttackWindowStart;
    public float AttackWindowEnd => _nextAttackWindowEnd;
    public float AttackDamageWindowStart=>_attackDamageStart;
    public float AttackDamageWindowEnd => _attackDamageEnd;
    public int Damage => _damage;
    [SerializeField] AnimationClip _associatedAnimation;
    [SerializeField] int _damage;
    [SerializeField] bool _useFrames;
    [SerializeField] float _nextAttackWindowStart;
    [SerializeField] float _nextAttackWindowEnd;
    [SerializeField] int _nextAttackWindowStartFrame;
    [SerializeField] int _nextAttackWindowEndFrame;
    [SerializeField] int _attackDamageStartFrame;
    [SerializeField] int _attackDamageEndFrame;
    [SerializeField] float _attackDamageStart;
    [SerializeField] float _attackDamageEnd;

    private void OnValidate()
    {
        if(_useFrames)
        {
            if (_associatedAnimation != null)
            {
                int framesInAnimation = (int)(_associatedAnimation.frameRate * _associatedAnimation.length);
                _nextAttackWindowStart = (_nextAttackWindowStartFrame / (float)framesInAnimation) * _associatedAnimation.length;
                _nextAttackWindowEnd = (_nextAttackWindowEndFrame / (float)framesInAnimation) * _associatedAnimation.length;

                _attackDamageStart = (_attackDamageStartFrame / (float)framesInAnimation) * _associatedAnimation.length;
                _attackDamageEnd = (_attackDamageEndFrame / (float)framesInAnimation) * _associatedAnimation.length;
            }
            else Logger.Error("No animation associated with attack", this);
        }
    }
}
