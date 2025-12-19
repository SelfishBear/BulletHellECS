using UnityEngine;

namespace CodeBase.Infrastructure.Services.Cursor
{
    public class CursorService : ICursorService
    {
        public void SetCursorState(bool isVisible, bool isLocked)
        {
            UnityEngine.Cursor.visible = isVisible;
            UnityEngine.Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}

