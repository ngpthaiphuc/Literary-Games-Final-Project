using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectivePickupItem : Objective
    {
        [Tooltip("Item to pickup to complete the objective")]
        public GameObject ItemToPickup;

        public GameObject nextObjective;

        protected override void Start()
        {
            // if(priorObjective == null){
            base.Start();

            EventManager.AddListener<PickupEvent>(OnPickupEvent);
            // } else{
            //     base.LimitedStart();
            //     if(priorObjective.IsCompleted){
            //         DisplayMessageEvent displayMessage = Events.DisplayMessageEvent;
            //         displayMessage.Message = Title;
            //         displayMessage.DelayBeforeDisplay = 0.0f;
            //         EventManager.Broadcast(displayMessage);

            //         EventManager.AddListener<PickupEvent>(OnPickupEvent);
            //     }
            // }
        }

        void OnPickupEvent(PickupEvent evt)
        {
            if (IsCompleted || ItemToPickup != evt.Pickup)
                return;

            // this will trigger the objective completion
            // it works even if the player can't pickup the item (i.e. objective pickup healthpack while at full heath)
            CompleteObjective(string.Empty, string.Empty, "Objective complete : " + Title);

            if (gameObject)
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<PickupEvent>(OnPickupEvent);
            nextObjective.SetActive(true);
        }
    }
}