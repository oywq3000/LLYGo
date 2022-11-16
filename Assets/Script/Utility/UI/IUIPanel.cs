using Script.Abstract;

namespace Script.UI
{
    public interface IUIPanel : IPoolable
    {
        void OnOpen();
    }
}