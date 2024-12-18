using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DungeonBlockPlacer : MonoBehaviour
{
    [Serializable]
    public class ListOfGameObjects
    {
        public List<GameObject> gameObjects= new List<GameObject>();
    }

    [SerializeField] List<ListOfGameObjects> _blocks = new List<ListOfGameObjects>();
    [SerializeField] Transform _blocksHolder;
    [SerializeField] GameObject _blockPrefab;
    public List<ListOfGameObjects> Blocks { get => _blocks; }

    public bool TryCreateBlock(Vector3 pos,Vector3 scale,Vector2Int index)
    {
        
        int yIndex = index.y;
        if (index.y >= _blocks.Count) 
        {
            _blocks.Add(new ListOfGameObjects());
            yIndex=_blocks.Count-1;
        }
        else
        {
            if (Blocks[yIndex].gameObjects.Count - 1 > index.x) return false;
        }
        GameObject el = Instantiate(_blockPrefab);
        el.name = $"block {yIndex},{index.x}";
        Blocks[yIndex].gameObjects.Add(el);
        el.transform.SetParent(_blocksHolder);
        el.transform.position = pos;
        el.transform.localScale = scale;
        return true;
    }
    public void RemoveBlocksOutsideGrid(Vector2 gridSize)
    {
        for(int i= _blocks.Count-1; i>=0;i--) 
        {
            for(int j = _blocks[i].gameObjects.Count-1; j >=0;j--) 
            {
                if(j>=gridSize.x || i >= gridSize.y)
                {
                    DestroyImmediate(_blocks[i].gameObjects[j]);
                     _blocks[i].gameObjects.RemoveAt(j);
                }
            }
            if(i >= gridSize.y) _blocks.RemoveAt(i);
        }
    }
    public void RemoveElement()
    {
        //DestroyImmediate(Blocks[Blocks.Count - 1]);

    }
    public void RemoveElementAtPos(int index)
    {
        //DestroyImmediate(Blocks[index]);
        //Blocks[index] = null;
        //for (int i = 0; i < Blocks.Count; i++)
        //{
        //    if (i >= index)
        //    {
        //        Blocks[i].GetComponent<DungeonBlock>().Setup(i);
        //        Blocks[i].name = "minimap element" + (i + 1);
        //    }
        //}
    }
}
