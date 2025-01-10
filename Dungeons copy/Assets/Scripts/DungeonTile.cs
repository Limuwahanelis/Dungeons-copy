using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public Action<DungeonTile> OnTileDug;
    public enum DungeonTileState
    {
        EMPTY,FILLED,DUNGEON_HEART
    }
    public bool Dig => _toDig;
    public DungeonTileState State => _state;
    public Vector3 TopBlockPos => _blockTopTran.position;
    [SerializeField] float _digTime;
    [SerializeField] DungeonTileState _state;
    [SerializeField] GameObject _filledBlock;
    [SerializeField] Transform _blockTopTran;
    private bool _toDig = false;
    private Coroutine _digCor=null;
    public void SetTileState(DungeonTileState state)
    {
        _state = state;
    }

    public void SetDigState(bool value)
    {
        _toDig = value;
        if(_toDig)
        {

            if (_digCor == null)
            {
                _digCor = StartCoroutine(DigCor());
            }
        }
        else
        {
            if (_digCor!=null)
            {
                StopCoroutine(_digCor);
                _digCor = null;
            }
        }
    }
    private void OnValidate()
    {
        switch (_state)
        {
            case DungeonTileState.EMPTY: GetComponentInChildren<MeshRenderer>().enabled = false; break;
            case DungeonTileState.FILLED: GetComponentInChildren<MeshRenderer>().enabled = true; break;
            case DungeonTileState.DUNGEON_HEART: GetComponentInChildren<MeshRenderer>().enabled = false; break;
        }
    }

    private IEnumerator DigCor()
    {
        yield return new WaitForSeconds(_digTime);
        _state = DungeonTileState.EMPTY;
        _filledBlock.GetComponent<MeshRenderer>().enabled = false;
        OnTileDug?.Invoke(this);
        _digCor = null;
    }
}
