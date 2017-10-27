using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScript : MonoBehaviour
{
    public int entero;
    public float dec;
    public string frase;
    public GameObject go;
    public bool isTrue;

    public void Write()
    {
        Debug.Log(frase);
    }
}
