
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SelectButton : UdonSharpBehaviour
{

    public Material defaultMaterial;
    public Material selectedMaterial;
    public GameObject buttonsParent;

    private bool selected = false;

    void Start()
    {
        
    }

    private void Interact() 
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (!selected)
        {
            meshRenderer.material = selectedMaterial;
            selected = true;
            setOthersToDefault();
        }
        else
        {
            meshRenderer.material = defaultMaterial;
            selected = false;
        }
    }

    public void setOthersToDefault() 
    {
        foreach (Transform child in buttonsParent.transform)
        {
            if (child.gameObject != gameObject) 
            {
                MeshRenderer meshRenderer = child.gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer.material.color == selectedMaterial.color)
                {
                    meshRenderer.material = defaultMaterial;
                    SelectButton selBtn = child.gameObject.GetComponent<SelectButton>();
                    selBtn.setSelected(false);
                }
            }
        }
    }

    public bool isSelected() 
    {
        return selected;
    }

    public void setSelected(bool val)
    {
        selected = val;
    }
}
