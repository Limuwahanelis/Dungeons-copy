using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Search;

// Change "PoolItem" to type of object to make pool for
public class  SelectionTilePool: MonoBehaviour
{
    [SerializeField] SelectionTile _itemPrefab;
    [SerializeField] Material _selectTileMat;
    private ObjectPool<SelectionTile> _pool;

    // Start is called before the first frame update
    void Awake()
    {
        _pool = new ObjectPool<SelectionTile>(CreateItem,OnTakeItemFromPool,OnReturnItemToPool);
    }

    public SelectionTile GetItem()
    {
        return _pool.Get();
    }
    SelectionTile CreateItem()
    {
        SelectionTile item = Instantiate(_itemPrefab);
        item.SetPool(_pool);
        return item;

    }
    void OnTakeItemFromPool(SelectionTile item)
    {
        item.gameObject.SetActive(true);
    }
    void OnReturnItemToPool(SelectionTile item)
    {
        item.gameObject.SetActive(false);
        item.SetMaterial( _selectTileMat );
        item.SetisPlaced(false);
    }
}
