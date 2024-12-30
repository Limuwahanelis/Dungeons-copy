using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SelectionTile : MonoBehaviour
{
   private  IObjectPool<SelectionTile> _pool;
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
}
