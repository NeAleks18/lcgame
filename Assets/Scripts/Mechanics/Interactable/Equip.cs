using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mechanics.Interactable
{
    public class Equip : BaseInteractable
    {
        private void Awake()
        {
            IsScrap = true;
            TimeToUse = 1f;
        }
        public override void Interact()
        {
            base.Interact();
            PickUp();
            
        }
        public void PickUp()
        {

            gameObject.SetActive(false);
        }
    }
}
