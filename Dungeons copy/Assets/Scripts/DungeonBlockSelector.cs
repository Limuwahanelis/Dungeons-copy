using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DungeonBlockSelector : MonoBehaviour
{
    public bool IsHittingMap => _isHittingMap;
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] LayerMask _mask;
    [SerializeField] TileMapSetter _map;
    [SerializeField] DungeonBlockPlacer _blockPlacer;
    [SerializeField] SelectionTilePool _tilePool;
    private const float _ROUND_ERROR_= 0.001f;
    private List<SelectionTile> _tiles=new List<SelectionTile>();
    private List<SelectionTile> _allSelectedTiles= new List<SelectionTile>();
    private DungeonTile _blockPointedAt;
    private Vector3 _lastPos;
    private Vector3 _mouseHoldStartPos;
    private Vector2 _mousePos;
    private Vector3 _hitPos;
    private Ray r;
    private Camera _cam;
    private float _gridMult = 1;
    private bool _isHittingMap;
    private bool _canHitMap = true;
    private bool _isHoldingMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _lastPos.x = 0;
        _lastPos.z = 0;
        _tilePrefab.transform.position = new Vector3(0, 0.001f, 0);
        _tilePrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canHitMap) return;

        RaycastHit hit;
        r = _cam.ScreenPointToRay(_mousePos);
        if (_isHittingMap = Physics.Raycast(r, out hit, Mathf.Infinity, _mask))
        {
            SelectTiles(hit);
        }
    }
    private void SelectTiles(RaycastHit hit)
    {

        _hitPos.x = Mathf.Round(hit.point.x);
        _hitPos.z = Mathf.Round(hit.point.z);
        if (_lastPos.x != _hitPos.x || _lastPos.z != _hitPos.z)
        {
           

            _blockPointedAt = hit.transform.parent.gameObject.GetComponent<DungeonTile>();
            _tilePrefab.transform.position = new Vector3(_hitPos.x, 0.001f, _hitPos.z);
            if (_isHoldingMouse)
            {
                Logger.Log("DIF");
                for (int i = _tiles.Count - 1; i >= 0; i--)
                {
                    _tiles[i].ReturnToPool();
                    _tiles.RemoveAt(i);
                }
                Logger.Log("New selection");
                float maxX = 0;
                float minX = 0;
                float maxZ = 0;
                float minZ = 0;
                maxX = math.max(_hitPos.x, _mouseHoldStartPos.x);
                minX = math.min(_hitPos.x, _mouseHoldStartPos.x);
                maxZ = math.max(_hitPos.z, _mouseHoldStartPos.z);
                minZ = math.min(_hitPos.z, _mouseHoldStartPos.z);
                Logger.Log($"Range x: {minX} {maxX} z: {minZ} {maxZ}");
                float index = minX;
                float index2 = minZ;
                Vector3 newTilePos = Vector3.zero;
                while (index2 <= maxZ)
                {
                    ListOfGameObjects blocksList = _blockPlacer.Blocks.Find(x => x.gameObjects.Find(y => math.abs(y.transform.position.z - index2) < _ROUND_ERROR_));
                    
                    for (int i = 0; i < blocksList.gameObjects.Count; i++)
                    {
                        if (blocksList.gameObjects[i].transform.position.x > maxX) break;
                        if (blocksList.gameObjects[i].transform.position.x < minX) continue;
                        newTilePos = new Vector3(blocksList.gameObjects[i].transform.position.x, 1.001f, index2);
                        if (_allSelectedTiles.Find(x => IsPositionSimilar(x.transform.position, newTilePos))) continue;
                        Logger.Log($" {blocksList.gameObjects[i].transform.position.x},{index2}");
                        SelectionTile tile = _tilePool.GetItem();
                        _tiles.Add(tile);
                        tile.transform.position = newTilePos;
                    }
                    index2 += _gridMult;
                }
            }
            _lastPos.x = _hitPos.x;
            _lastPos.z = _hitPos.z;
        }
    }
    private bool IsPositionSimilar(Vector3 pos1, Vector3 pos2)
    {
        if(math.abs(pos1.x - pos2.x)<0.01f)
        {
            if(math.abs(pos1.z-pos2.z)<0.01f)
            {
                return true;
            }
        }
        return false;
    }
    public void SetMousePos(Vector2 pos)
    {
        _mousePos = pos;

    }
    public void PlaceTile()
    {
        if (_blockPointedAt.Dig) return;
        _blockPointedAt.SetDigState(true);
        SelectionTile tile = _tilePool.GetItem();
        _tiles.Add(tile);
        tile.transform.position = _blockPointedAt.TopBlockPos;
    }
    public void SetCanHitMap(bool value)
    {
        _canHitMap = value;
        if (!_canHitMap) _isHittingMap = false;
    }
    public void StartTileHold()
    {
        _isHoldingMouse = true;
        _mouseHoldStartPos = _blockPointedAt.TopBlockPos;
        PlaceTile();
    }
    public void StopTileHold()
    {
        _isHoldingMouse = false;
        
        for (int i = _tiles.Count - 1; i >= 0; i--)
        {
            _allSelectedTiles.Add(_tiles[i]);
           // _tiles[i].ReturnToPool();
            _tiles.RemoveAt(i);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r.origin, r.direction * 20f);
    }
}
