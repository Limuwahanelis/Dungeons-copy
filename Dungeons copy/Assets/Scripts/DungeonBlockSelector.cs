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
    [SerializeField] LayerMask _tilesLayersmask;
    [SerializeField] TileMapSetter _map;
    [SerializeField] DungeonBlockPlacer _blockPlacer;
    [SerializeField] SelectionTilePool _tilePool;
    [SerializeField] Material _placedTileSelectorMat;
    [SerializeField] Material _currentlySelectedTileMat;
    [SerializeField] Material _removeTileMat;
    private const float _ROUND_ERROR_= 0.001f;
    private List<SelectionTile> _tiles=new List<SelectionTile>();
    private List<SelectionTile> _allSelectedTiles= new List<SelectionTile>();
    private List<SelectionTile> _tilesToRemve= new List<SelectionTile>();
    private List<DungeonTile> _allSelectedBlocks= new List<DungeonTile>();
    private List<DungeonTile> _dungeonBlocks = new List<DungeonTile> ();
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
    private bool _isHitingTile;
    // TODO: change spawning green selection tile to already spawned one.
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _lastPos.x = 0;
        _lastPos.z = 0;
        _tilePrefab.transform.position = new Vector3(0, 1.001f, 0);
        _tilePrefab.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canHitMap) return;

        RaycastHit hit;
        r = _cam.ScreenPointToRay(_mousePos);

        if (!_isHoldingMouse)
        {
            if (Physics.Raycast(r, out hit, Mathf.Infinity, _tilesLayersmask))
            {
                SelectionTile tile = hit.collider.GetComponent<SelectionTile>();
                if (tile.IsPlaced) _isHitingTile = true;
                else _isHitingTile = false;
            }
            else _isHitingTile = false;
        }
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
           //if(_tiles.Count == 0) 
           // {
           //         _tiles[0].ReturnToPool();
           //         _tiles.RemoveAt(0);
           // }

            _blockPointedAt = hit.transform.parent.gameObject.GetComponent<DungeonTile>();
            _tilePrefab.transform.position = new Vector3(_hitPos.x, 1.001f, _hitPos.z);
            PlaceTile();
            if (_isHoldingMouse)
            {
                Logger.Log("DIF");
                if(_isHitingTile)
                {
                    
                    for (int i=0;i< _allSelectedTiles.Count;i++)
                    {
                        _allSelectedTiles[i].SetMaterial(_placedTileSelectorMat);
                        if (_tilesToRemve.Contains(_allSelectedTiles[i])) _tilesToRemve.Remove(_allSelectedTiles[i]);

                    }
                }
                for (int i = _tiles.Count - 1; i >= 0; i--)
                {
                    _tiles[i].ReturnToPool();
                    _tiles.RemoveAt(i);
                }
                for(int i=_dungeonBlocks.Count - 1; i >= 0;i--)
                {
                    _dungeonBlocks.RemoveAt(i);
                }
                Logger.Log("New selection");

                float minX;
                float minZ;
                float maxX;
                float maxZ;
                SetMinMaxValues(_hitPos.x, _mouseHoldStartPos.x, out minX, out maxX);
                SetMinMaxValues(_hitPos.z, _mouseHoldStartPos.z, out minZ, out maxZ);
                float index = minX;
                float index2 = minZ;
                Logger.Log($"Range x: {minX} {maxX} z: {minZ} {maxZ}");

                Vector3 newTilePos = Vector3.zero;
                while (index2 <= maxZ)
                {
                    ListOfGameObjects blocksList = _blockPlacer.Blocks.Find(x => x.gameObjects.Find(y => math.abs(y.transform.position.z - index2) < _ROUND_ERROR_));
                    
                    for (int i = 0; i < blocksList.gameObjects.Count; i++)
                    {
                        DungeonTile dungTile = blocksList.gameObjects[i].GetComponent<DungeonTile>();
                        if (blocksList.gameObjects[i] == null) continue;
                        if (blocksList.gameObjects[i].transform.position.x > maxX) continue;
                        if (blocksList.gameObjects[i].transform.position.x < minX) continue;
                        if (dungTile.State != DungeonTile.DungeonTileState.FILLED) continue;

                        newTilePos = new Vector3(blocksList.gameObjects[i].transform.position.x, 1.001f, index2);
                        SelectionTile placedTile = _allSelectedTiles.Find(x => IsPositionSimilar(x.transform.position, newTilePos));
                        if (placedTile != null)
                        {
                            if (_isHitingTile)
                            {
                                placedTile.SetMaterial(_removeTileMat);
                                if (_tilesToRemve.Contains(placedTile)) continue;
                                _tilesToRemve.Add(placedTile);
                            }
                            else continue;
                        }
                        else
                        {
                            SelectionTile tile = _tilePool.GetItem();
                            _tiles.Add(tile);
                            _dungeonBlocks.Add(dungTile);
                            tile.transform.position = newTilePos;
                            if (_isHitingTile) tile.SetMaterial(_removeTileMat);
                        }
                    }
                    index2 += _gridMult;
                }
            }
            _lastPos.x = _hitPos.x;
            _lastPos.z = _hitPos.z;
        }
    }
    private bool TrySelectTile(GameObject tileObject, float tileZpos, Vector3 newTilePos,float tileMaxXPos, float tileMinXPos)
    {
        if (tileObject == null) return false;
        if (tileObject.transform.position.x > tileMaxXPos) return false;
        if (tileObject.transform.position.x < tileMinXPos) return false;
        if (tileObject.GetComponent<DungeonTile>().State == DungeonTile.DungeonTileState.DUNGEON_HEART) return false;
        newTilePos = new Vector3(tileObject.transform.position.x, 1.001f, tileZpos);
        SelectionTile placedTile = _allSelectedTiles.Find(x => IsPositionSimilar(x.transform.position, newTilePos));
        if (placedTile != null)
        {
            if (_isHitingTile)
            {
                placedTile.SetMaterial(_removeTileMat);
                if (_tilesToRemve.Contains(placedTile)) return false;
                _tilesToRemve.Add(placedTile);
            }
            else return false;
        }
        else
        {
           // Logger.Log($" {tileObject.transform.position.x},{index2}");
            SelectionTile tile = _tilePool.GetItem();
            _tiles.Add(tile);
            tile.transform.position = newTilePos;
            if (_isHitingTile) tile.SetMaterial(_removeTileMat);
        }
        return true;
    }
    private void SetMinMaxValues(float x1,float x2,out float minX, out float maxX)
    {
        maxX = math.max(x1, x2);
        minX = math.min(x1, x2);
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
    public void ClickDungeonTile()
    {
        if (_isHitingTile)
        {
            SelectionTile placedTile = _allSelectedTiles.Find(x => IsPositionSimilar(_blockPointedAt.TopBlockPos, x.transform.position));
            placedTile.ReturnToPool();
            _allSelectedTiles.Remove(placedTile);
            _blockPointedAt.SetDigState(false);
        }
        else
        {
            if (_blockPointedAt.State != DungeonTile.DungeonTileState.FILLED) return;
            SelectionTile tile = _tilePool.GetItem();
            _allSelectedTiles.Add(tile);
            _isHitingTile = true;
            tile.SetisPlaced(true);
            tile.SetMaterial(_placedTileSelectorMat);
            tile.transform.position = _blockPointedAt.TopBlockPos;
            _blockPointedAt.SetDigState(true);
            _blockPointedAt.OnTileDug += RemoveTile;
        }

    }
    public void PlaceTile()
    {
       // SelectionTile tile = _tilePool.GetItem();
        if(_isHitingTile) _tilePrefab.GetComponent<SelectionTile>().SetMaterial( _removeTileMat );
        else _tilePrefab.GetComponent<SelectionTile>().SetMaterial(_currentlySelectedTileMat );
       // _tiles.Add(tile);
        //tile.transform.position = _blockPointedAt.TopBlockPos;
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
        //PlaceTile();
    }
    public void StopTileHold()
    {
        _isHoldingMouse = false;
        if (_tiles.Count == 0)
        {
            if (_allSelectedTiles.Find(x => IsPositionSimilar(x.transform.position, _tiles[0].transform.position)))
            {
                if (_isHitingTile)
                {
                    _allSelectedTiles.Remove(_tiles[0]);
                    _tiles[0].ReturnToPool();
                    _tiles.RemoveAt(0);
                }
                else return;

            }
        }
        if (_isHitingTile)
        {
            for(int i= _tilesToRemve.Count-1; i>=0;i--)
            {
                _allSelectedTiles.Remove(_tilesToRemve[i]);
                _tilesToRemve[i].ReturnToPool();
                _tilesToRemve.RemoveAt(i);
            }
            for (int i = _tiles.Count - 1; i >= 0; i--)
            {

                _tiles[i].ReturnToPool();
                _tiles.RemoveAt(i);
            }
        }
        else
        {
            for (int i = _tiles.Count - 1; i >= 0; i--)
            {
                _allSelectedBlocks.Add(_dungeonBlocks[i]);
                _dungeonBlocks[i].SetDigState(true);
                _dungeonBlocks[i].OnTileDug += RemoveTile;
                _allSelectedTiles.Add(_tiles[i]);
                _tiles[i].SetMaterial(_placedTileSelectorMat);
                _tiles[i].SetisPlaced(true);
                _tiles.RemoveAt(i);
            }
        }
        // to allow selecting same tile when ending hold
        _lastPos.x = _hitPos.x + 20f;
    }
    private void RemoveTile(DungeonTile dungTile)
    {
        SelectionTile tile= _allSelectedTiles.Find(x => IsPositionSimilar(dungTile.TopBlockPos, x.transform.position));
        
        _allSelectedTiles.Remove(tile);
        tile.ReturnToPool();
        dungTile.OnTileDug -= RemoveTile;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r.origin, r.direction * 20f);
    }
}
