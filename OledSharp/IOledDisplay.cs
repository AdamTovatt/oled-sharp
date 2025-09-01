namespace OledSharp
{
    internal interface IOledDisplay : IDisposable
    {
        int Width { get; }
        int Height { get; }
        void SetPixel(int x, int y, bool isOn);
        bool GetPixel(int x, int y);
        void Clear();
        void Update();
        void ClearAndUpdate();
    }
}
