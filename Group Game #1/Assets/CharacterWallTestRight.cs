using UnityEngine;

public class CharacterWallTestRight : MonoBehaviour
{
    public bool IsWalledRight = false;

    void OnTriggerStay2D(Collider2D other)
    {
        IsWalledRight = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        IsWalledRight = false;
    }

}
