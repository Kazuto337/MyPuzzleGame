using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Matrix_DTO
{
    public List<MapItem> map;
}
[System.Serializable]
public class MapItem
{
    public string coordinates;
    public int value;
}
