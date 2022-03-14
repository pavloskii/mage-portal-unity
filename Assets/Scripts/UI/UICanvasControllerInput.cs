using UnityEngine;

namespace StarterAssets
{
    public class UICanvasControllerInput : MonoBehaviour
    {
        [Header("Output")]
        public PlayerController playerController;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            playerController.MoveInput(virtualMoveDirection);
        }
    }

}
