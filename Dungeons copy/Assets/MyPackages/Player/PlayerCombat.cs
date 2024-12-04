using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public enum AttackModifiers
    {

        NONE, UP_ARROW, DOWN_ARROW,
    }
    public enum AttackType
    {
        FIST1, FIST2, FIST3
    }

#if UNITY_EDITOR
    [Header("Debug"), SerializeField] bool _debug;
#endif
    public ComboList PlayerCombos => _comboList;
    public ComboList PlayerAirCombos => _airComboList;
    public ComboAttack JumpAttack => _airJumpAttack;
    [Header("Combat"), SerializeField] LayerMask enemyLayer;
    public float SlamSpeed;

    [SerializeField] int attackDamage;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Sprite _playerHitSprite;
    [SerializeField] Sprite _playerCrouchedhitSprite;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] AnimationManager _animMan;
    [Header("Attacks")]

    [SerializeField] ComboList _comboList;
    [SerializeField] ComboList _airComboList;
    [SerializeField] ComboAttack _airJumpAttack;

    [Header("Attacks positions")]

    [SerializeField] Transform _fistAttack1Pos;
    [SerializeField] Transform _fistAttack2Pos;
    [SerializeField] Transform _fistAttack3Pos;
    [SerializeField] Transform _airSlamLandingAttackPos;
    [Header("Attacks sizes")]
    [SerializeField] Vector2 _fistAttack1Size;
    [SerializeField] Vector2 _fistAttack2Size;
    [SerializeField] Vector2 _fistAttack3Size;
    [SerializeField] Vector2 _airSlamLandingAttackSize;

    private Coroutine airAttackCor;
    private Coroutine playerMovAirAttackCor;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StopAttack()
    {
        StopAllCoroutines();
    }
    public void StopAirAttack()
    {
        StopCoroutine(airAttackCor);
        StopCoroutine(playerMovAirAttackCor);
        //_player.playerMovement.SetGravityScale(2);
    }
    public void ChangeSpriteToPushed()
    {
        _spriteRenderer.sprite = _playerHitSprite;
    }
    public void ChangeSpriteToCoruchPushed()
    {
        _spriteRenderer.sprite = _playerCrouchedhitSprite;
    }
    public IEnumerator AttackCor(AttackType attackType)
    {
        List<Collider2D> hitEnemies = new List<Collider2D>();
        switch (attackType)
        {
            case AttackType.FIST1: hitEnemies = Physics2D.OverlapBoxAll(_fistAttack1Pos.position, _fistAttack2Size, 0, enemyLayer).ToList(); break;
            case AttackType.FIST2: hitEnemies = Physics2D.OverlapBoxAll(_fistAttack2Pos.position, _fistAttack2Size, 0, enemyLayer).ToList(); break;
            case AttackType.FIST3: hitEnemies = Physics2D.OverlapBoxAll(_fistAttack3Pos.position, _fistAttack3Size, 0, enemyLayer).ToList(); break;
        }


        int index = 0;
        for (; index < hitEnemies.Count; index++)
        {
            IDamagable tmp = hitEnemies[index].GetComponentInParent<IDamagable>();
            if (tmp != null) tmp.TakeDamage(new DamageInfo(_comboList.comboList[((int)attackType)].Damage, transform.position));
        }
        yield return null;
        while (true)
        {
            Collider2D[] colliders = null;
            switch (attackType)
            {
                case AttackType.FIST1: colliders = Physics2D.OverlapBoxAll(_fistAttack1Pos.position, _fistAttack2Size, 0, enemyLayer); break;
                case AttackType.FIST2: colliders = Physics2D.OverlapBoxAll(_fistAttack2Pos.position, _fistAttack2Size, 0, enemyLayer); break;
                case AttackType.FIST3: colliders = Physics2D.OverlapBoxAll(_fistAttack3Pos.position, _fistAttack3Size, 0, enemyLayer); break;
            }
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!hitEnemies.Contains(colliders[i]))
                {
                    hitEnemies.Add(colliders[i]);
                    IDamagable tmp = colliders[i].GetComponentInParent<IDamagable>();
                    if (tmp != null) tmp.TakeDamage(new DamageInfo(_comboList.comboList[((int)attackType)].Damage, transform.position));
                }
            }
            yield return null;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_debug)
        {
            if (_fistAttack1Pos != null) Gizmos.DrawWireCube(_fistAttack1Pos.position, _fistAttack1Size);
            if (_fistAttack2Pos != null) Gizmos.DrawWireCube(_fistAttack2Pos.position, _fistAttack2Size);
            if (_fistAttack3Pos != null) Gizmos.DrawWireCube(_fistAttack3Pos.position, _fistAttack3Size);
            if (_airSlamLandingAttackPos != null) Gizmos.DrawWireCube(_airSlamLandingAttackPos.position, _airSlamLandingAttackSize);
        }
    }
#endif
}
