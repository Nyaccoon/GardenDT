using UnityEngine;

public class ObjectDTO 
{
   ObjectDTO(Vector3 pos, int typeIndex)
   {
      position = pos;
      type = typeIndex;
   }
   
   public int type { get; set; }
   public Vector3 position { get; set; }
}
