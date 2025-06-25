using UnityEngine;
using Zenject;

public class Checkpoint : MonoBehaviour
{

   private CheckpointBehaviour _behaviour;
   
   [Inject] 
   private void Construct(CheckpointBehaviour behaviour)
   {
      _behaviour = behaviour;
   }
   private void OnTriggerEnter(Collider other)
   {
      if (!other.CompareTag("Player")) return;
      Debug.Log("Checkpoint: " + other.gameObject.name);
   }
}
