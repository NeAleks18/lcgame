using UnityEngine;

namespace Mechanics.Scanner
{
    [CreateAssetMenu(fileName = "AboutItem", menuName = "GamePlay/ScannerData")]
    public class ScannerData : ScriptableObject
    {
        public short Cost;
        public string NameOfItem;
        public float Distance = 4f;
    }
}