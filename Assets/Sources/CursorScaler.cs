using UnityEngine;

public class CursorScaler : MonoBehaviour
{
    [SerializeField] private Texture2D texture;

    private void Start()
    {
        CursorMode mode = CursorMode.ForceSoftware;
        Vector2 hotSpot = new Vector2(texture.width, texture.height) / 2f;
        Cursor.SetCursor(texture, hotSpot, mode);
    }
}
