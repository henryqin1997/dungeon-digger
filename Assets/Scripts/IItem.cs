using UnityEngine;

public interface IItem
{
    // Start is called before the first frame update
    string GetName();
    Sprite GetIcon();
    Sprite GetFrame();
}
