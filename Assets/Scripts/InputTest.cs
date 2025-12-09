using UnityEngine;

public class InputTest : MonoBehaviour
{
    public InputReader input;

    void Start()
    {
        input.MoveEvent += HandleMove;
        input.JumpEvent += HandleJump;
    }

    void OnDestroy()
    {
        input.MoveEvent -= HandleMove;
        input.JumpEvent -= HandleJump;
    }

    private void HandleMove(Vector2 movement)
    {
        Debug.Log($"Moving: {movement}");
    }

    private void HandleJump()
    {
        Debug.Log("JUMP!");
    }
}