using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomToPlaceSelector : MonoBehaviour
{
    
    public RoomType SelectedRoomType { get => _selectedRoomType;}
    private RoomType _selectedRoomType;


    public void SetRoomTypeToPlace(RoomType type)
    {
        _selectedRoomType = type;
    }
}
