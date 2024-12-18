using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTile : MonoBehaviour
{
    public DungeonTileState State => _state;
    [SerializeField] DungeonTileState _state;
    [SerializeField] GameObject _filledBlock;

    public void SetTileState(DungeonTileState state)
    {
        _state = state;
    }
}
