using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pages_DTO
{
    public string message;
    public string imageName;
}
[System.Serializable]
public class Instructions_DTO
{
    public List<Pages_DTO> pages;
}
