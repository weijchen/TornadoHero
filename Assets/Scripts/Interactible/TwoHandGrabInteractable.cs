using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Team13.Round1.TornadoHero
{
    public class TwoHandGrabInteractable : XRGrabInteractable
    {
        public List<XRSimpleInteractable> secondHandGrabPoints = new List<XRSimpleInteractable>();
    
        private XRBaseInteractor secondInteractor;
        private Quaternion attachInitialRotation;
        
        void Start()
        {
            
            foreach (var item in secondHandGrabPoints)
            {
                item.onSelectEnter.AddListener(OnSecondHandGrab);
                item.onSelectExit.AddListener(OnSecondHandRelease);
            }
            
        }
    
        void Update()
        {
            
        }
        
        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (secondInteractor && selectingInteractor)
            {
                //compute the rotation
                selectingInteractor.attachTransform.rotation = Quaternion.LookRotation(
                    secondInteractor.attachTransform.position - selectingInteractor.attachTransform.position);
            }
            base.ProcessInteractable(updatePhase);
        }
    
        public void OnSecondHandGrab(XRBaseInteractor interactor)
        {
            secondInteractor = interactor;
        }
        
        public void OnSecondHandRelease(XRBaseInteractor interactor)
        {
            secondInteractor = null;
        }
    
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            attachInitialRotation = args.interactor.attachTransform.localRotation;
        }
    
        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            secondInteractor = null;
            args.interactor.attachTransform.localRotation = attachInitialRotation;
        }
        
        
        public override bool IsSelectableBy(XRBaseInteractor interactor)
        {
            bool isAlreadyGrabbed = selectingInteractor && !interactor.Equals(selectingInteractor);
            return base.IsSelectableBy(interactor) && !isAlreadyGrabbed;
        }
    }
}

