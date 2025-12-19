namespace CodeBase.Infrastructure.Services.Cursor
{
    public interface ICursorService
    {
        void SetCursorState(bool isVisible, bool isLocked);
    }
}