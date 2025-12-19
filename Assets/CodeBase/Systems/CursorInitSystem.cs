using CodeBase.Infrastructure.Services.Cursor;
using Leopotam.Ecs;

namespace CodeBase.Systems
{
    public class CursorInitSystem : IEcsInitSystem
    {
        private ICursorService _cursorService;

        public void Init()
        {
            _cursorService.SetCursorState(isVisible: false, isLocked: false);
        }
    }
}

