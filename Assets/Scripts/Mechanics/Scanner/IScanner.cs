namespace Mechanics.Scanner
{
    public interface IScanner
    {
        // Data
        Item Item { get; }
        TMPro.TextMeshProUGUI ForName { get; }
        TMPro.TextMeshProUGUI ForCost { get; }
        UnityEngine.GameObject Image { get; }
        float Distance { get; }
        float Scale { get; }

        // Funcs
        void Init();
        void Always();
        void Show();
        void Hide();
    }
}