using DG.Tweening;
using UnityEngine;

namespace Mechanics
{
    public class Ship : MonoBehaviour
    {
        public string ShipLandTag = "ShipLandTransform";

        [SerializeField]
        private short LandTime = 4;

        private void Start()
        {
            LandShip(GameObject.FindGameObjectWithTag(ShipLandTag).transform);
        }

        public void LandShip(Transform LandTransform)
        {
            gameObject.transform.DOMove(LandTransform.position, LandTime);
        }
    }
}