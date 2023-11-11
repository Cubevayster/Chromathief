using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI keyText;

    void Start()
    {
        keyText.SetText("Cl� de sortie manquante");
    }

    void OnTriggerEnter( Collider collider)
    {
        PlayerControler controller =  collider.GetComponent<PlayerControler>();
        if (controller)
        {
            controller.FoundExitKey = true;
            keyText.SetText("Cl� de sortie trouv�");
            Destroy(this.gameObject);
        }
    }
}
