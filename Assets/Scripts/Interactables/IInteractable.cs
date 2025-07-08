namespace Magic.Interaction
{
    public interface IInteractable
    {

        public InteractableInfo GetInfo();
        //public InteractableInfo InteractableInfo { get; set; }
        public void Interact();

       
    }
}
