using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ringhandle : MonoBehaviour
{
    public Transform handle;
    private Vector3 normalized;
    private void Update()
    {
        if (handle == null) return;
        normalized = Vector3.Normalize(handle.transform.position - transform.position);
        Vector3 handlePos = transform.position + normalized*2; // multiplied by 2 for better visibility
        handle.transform.position = handlePos;
    }

    public Vector3 GetVector()
    {
        return normalized / 2;
    }

}
