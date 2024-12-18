using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DungeonBlock : MonoBehaviour
{
    int listIndex;
    private void OnDestroy()
    {
        if (Time.frameCount == 0) return;

        if (GameObject.FindGameObjectWithTag("DungeonBlockPlayer"))
        {
            GameObject.FindGameObjectWithTag("Minimap").GetComponent<DungeonBlockPlacer>().RemoveElementAtPos(listIndex);
        }
    }

    public void Setup(int num)
    {
        listIndex = num;
    }
}
