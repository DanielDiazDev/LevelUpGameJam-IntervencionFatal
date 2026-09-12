using FMODUnity;
using UnityEngine;

public class FMODBankLoader : MonoBehaviour
{
    void Awake()
     {
         RuntimeManager.LoadBank("Master", true);
         RuntimeManager.LoadBank("Master.strings", true);
        
     }
}
