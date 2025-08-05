using UnityEngine;

public class Enemy : Interactable
{
    public override void Interact(float damage, Vector2 hitpoint)
    {
        Debug.Log("Interacted");
    }
}