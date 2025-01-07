using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public enum DungeonTileState
    {
        EMPTY,FILLED,DUNGEON_HEART
    }
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
    private void OnValidate()
    {
        switch (_state)
        {
            case DungeonTileState.EMPTY: GetComponentInChildren<MeshRenderer>().enabled = false; break;
            case DungeonTileState.FILLED: GetComponentInChildren<MeshRenderer>().enabled = true; break;
            case DungeonTileState.DUNGEON_HEART: GetComponentInChildren<MeshRenderer>().enabled = false; break;
        }
    }
}
