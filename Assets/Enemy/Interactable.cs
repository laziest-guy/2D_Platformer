using Unity.VisualScripting;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void Interact(float damage, Vector2 hitpoint) { }
}