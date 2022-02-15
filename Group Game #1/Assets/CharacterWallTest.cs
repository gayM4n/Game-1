using UnityEngine;

public class CharacterWallTest : MonoBehaviour
{
    public bool IsWalled;

    void OnTriggerStay2D(Collider2D other)
    {
        IsWalled = true;
        print("Walled");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IsWalled = false;
        print("Unwalled");
    }

}
