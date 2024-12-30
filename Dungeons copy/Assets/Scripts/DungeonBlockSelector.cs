using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DungeonBlockSelector : MonoBehaviour
{
    public Vector3 SelectedTilePos => _selectedTilePos;
    public bool IsHittingMap => _isHittingMap;
    //public MapTile SelectedTile => _selectedObject;
    [SerializeField] GameObject _tilePrefab;
    [SerializeField] LayerMask _mask;
    [SerializeField] TileMapSetter _map;
    [SerializeField] List<ListOfGameObjects> _tiles;
    [SerializeField] DungeonBlockPlacer _blockPlacer;
    private DungeonTile _blockPointedAt;
    private Vector3 _selectedTilePos;
    private Vector3 _lastPos;
    private bool _isHittingMap;
    private Vector2 _mousePos;
    private Ray r;
    private bool _canHitMap = true;
    private Camera _cam;
    private bool _isHoldingMouse = false;
    private Vector3 _mouseHoldStartPos;
    private Vector3 _mouseHoldCurrentPos;
    private float _gridMult = 1;
    private const float _ROUND_ERROR_= 0.001f;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _lastPos.x = 0;
        _lastPos.z = 0;
        _tilePrefab.transform.position = new Vector3(0, 0.001f, 0);
        _selectedTilePos.x = 0;
        _selectedTilePos.y = 0.001f;
        _selectedTilePos.z = 0;
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

            float posX = Mathf.Round(hit.point.x);
            float posZ = Mathf.Round(hit.point.z);
            if (_lastPos.x != posX || _lastPos.z != posZ)
            {
                _lastPos.x = posX;
                _lastPos.z = posZ;
                _blockPointedAt = hit.transform.parent.gameObject.GetComponent<DungeonTile>();
                _tilePrefab.transform.position = new Vector3(posX, 0.001f, posZ);
                _selectedTilePos.x = posX;
                _selectedTilePos.y = 0.001f;
                _selectedTilePos.z = posZ;
                if(_isHoldingMouse && (_mouseHoldStartPos.x!= posX || _mouseHoldStartPos.z != posZ))
                {
                    Logger.Log("New selection");
                    float maxX = 0;
                    float minX = 0;
                    float maxZ = 0;
                    float minZ = 0;
                    maxX = math.max(_lastPos.x, _mouseHoldStartPos.x);
                    minX = math.min(_lastPos.x, _mouseHoldStartPos.x);
                    maxZ = math.max(_lastPos.z, _mouseHoldStartPos.z);
                    minZ = math.min(_lastPos.z, _mouseHoldStartPos.z);
                    Logger.Log($"Range x: {minX} {maxX} z: {minZ} {maxZ}");
                    float index = minX;
                    float index2 = minZ;
                    while(index2 <= maxZ)
                    {
                        ListOfGameObjects blocksList= _blockPlacer.Blocks.Find(x => x.gameObjects.Find(y => math.abs(y.transform.position.z - index2) < _ROUND_ERROR_));
                        for(int i=0;i< blocksList.gameObjects.Count;i++)
                        {
                            if (blocksList.gameObjects[i].transform.position.x > maxX) break;
                            if (blocksList.gameObjects[i].transform.position.x < minX) continue;
                            Logger.Log($" {blocksList.gameObjects[i].transform.position.x},{index2}");
                        }
                        index2 += _gridMult;
                    }
                }
            }
            //_selectedObject = hit.transform.gameObject.GetComponent<MapTile>();
        }
    }
    public void SetMousePos(Vector2 pos)
    {
        _mousePos = pos;

    }
    public void PlaceTile()
    {
        if (_blockPointedAt.Dig) return;
        _blockPointedAt.SetDigState(true);
        GameObject go= Instantiate(_tilePrefab);
        go.transform.position = _blockPointedAt.TopBlockPos;
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
    }
    public void StopTileHold()
    {
        _isHoldingMouse = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(r.origin, r.direction * 20f);
    }
}
