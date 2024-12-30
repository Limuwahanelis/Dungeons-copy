using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileMapSetter : MonoBehaviour
{
    public Vector2 GridSize=>_gridSize;
    [SerializeField] GameObject _digPrefab;
   
    [SerializeField] Material _mat;
    [SerializeField] GameObject _mapQuad;
    [SerializeField] Vector2 _gridSize;
    [SerializeField, Range(2, 20)] int _horizontalSize;
    [SerializeField, Range(2, 20)] int _verticalSize;
    [SerializeField] DungeonBlockPlacer _blockPlacer;
    private Vector2 _oldGridSize=new Vector2(3,3);
    private MaterialPropertyBlock _materialBlock;
    private Renderer _renderer;
    private void Awake()
    {
        _materialBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
        //_renderer.SetPropertyBlock(_materialBlock);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdatGridHorizontally(float newSize)
    {

        _gridSize.x = (int)newSize;
        if (_gridSize == _oldGridSize) return;
        Vector3 halfSizeIncrease = new Vector3(-math.abs(_mapQuad.transform.localScale.x - _gridSize.x) / 2, 0, 0);
        halfSizeIncrease = new Vector3((_mapQuad.transform.localScale.x - _gridSize.x > 0 ? 1 : -1) * halfSizeIncrease.x, 0, 0);
        UpdateGrid(halfSizeIncrease);
    }

    public void UpdatGridVertically(float newSize)
    {
        _gridSize.y = (int)newSize;
        if (_gridSize == _oldGridSize) return;
        Vector3 halfSizeIncrease = new Vector3(0, 0, math.abs(_mapQuad.transform.localScale.y - _gridSize.y) / 2);
        halfSizeIncrease = new Vector3(0, 0, (_mapQuad.transform.localScale.y - _gridSize.y > 0 ? -1 : 1) * halfSizeIncrease.z);
        UpdateGrid(halfSizeIncrease);
    }
    private void UpdateGrid(Vector3 halfSizeIncrease)
    {
        _materialBlock.SetVector("_Vector2", _gridSize);
        _renderer.SetPropertyBlock(_materialBlock);
        _mapQuad.transform.localScale = _gridSize;
        _mapQuad.transform.position += halfSizeIncrease;
        _oldGridSize = _gridSize;
        for (int i = 0; i < _gridSize.y; i++)
        {
            for(int j=0;j<_gridSize.x; j++)
            {
                _blockPlacer.TryCreateBlock(new Vector3(j, 0, i), Vector3.one, new Vector2Int(j, i));

            }
        }
        _blockPlacer.RemoveBlocksOutsideGrid(GridSize);
    }
    private void OnValidate()
    {
        if(_materialBlock == null)
        {
            _materialBlock = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();
        }
        //UpdatGridHorizontally(_horizontalSize);
        //UpdatGridVertically(_verticalSize);
    }
}
