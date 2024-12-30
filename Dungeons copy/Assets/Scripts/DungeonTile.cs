using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public bool Dig => _toDig;
    public DungeonTileState State => _state;
    public Vector3 TopBlockPos => _blockTopTran.position;
    [SerializeField] DungeonTileState _state;
    [SerializeField] GameObject _filledBlock;
    [SerializeField] Transform _blockTopTran;
    private bool _toDig = false;
    public void SetTileState(DungeonTileState state)
    {
        _state = state;
    }

    public void SetDigState(bool value)
    {
        _toDig = value;
    }
}
