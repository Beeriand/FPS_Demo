using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;

    private PlayerUI playerUI;
    private InputManager inputManager;
    public GameObject weapon;
  
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;  
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

 
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        playerUI.UpdateText(string.Empty);
        playerUI.HealthBarUpdate(0, 0,false,0);

        if(Physics.Raycast(ray, out hit, distance, mask))
        {
            if (hit.collider.GetComponent<Interactable>())
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                playerUI.UpdateText(hit.collider.GetComponent<Interactable>().promptMessage);

                float fillSpeed = 5.0f * Time.deltaTime;
                playerUI.HealthBarUpdate(hit.collider.GetComponent<Interactable>().currentHP, hit.collider.GetComponent<Interactable>().maxHP, true, fillSpeed);

                if(hit.collider.tag == "PowerUP")
                {
                    playerUI.healthBar.SetActive(false);
                    weapon = GameObject.FindGameObjectWithTag("Weapon");

                    if (weapon.name == "Pistol")
                    {
                        playerUI.UpdateText(hit.collider.name);
                    }
                    else
                    {
                        playerUI.UpdateText(hit.collider.name + ": " + "Buffs are only available for the Pistol :/");
                    }
                }

                    if (inputManager.onFoot.Interact.triggered)
                    {
                        interactable.BaseInteraction();
                    }
            }
        }
    }
}
