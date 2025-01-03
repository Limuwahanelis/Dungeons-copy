using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SelectionTile : MonoBehaviour
{
    public bool IsPlaced => _isPlaced;
    private  IObjectPool<SelectionTile> _pool;
    private bool _isPlaced = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPool(IObjectPool<SelectionTile> pool)
    {
        _pool = pool;
    }
    public void ReturnToPool() => _pool.Release(this);
    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().sharedMaterial = mat;
    }
    public void SetisPlaced(bool isPlaced)
    {
        _isPlaced=isPlaced;
    }
}
