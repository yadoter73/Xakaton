using UnityEngine;

public interface IInteractable 
{
    void Interact(int id);
    string GetDescription(); 
    string GetState();
}
