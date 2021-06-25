public interface IFocusable
{
    void FocusAcquired(string acquiredFocusId);
    void FocusReleased(string releasedFocusId);
}
