namespace OledSharp
{
    public interface IOledDisplay : IDisposable
    {
        int Width { get; }
        int Height { get; }
        void Initialize();
        void SetBufferPixel(int x, int y, bool isOn);
        bool GetBufferPixel(int x, int y);
        void ClearBuffer();
        void PushBuffer();
    }
}
