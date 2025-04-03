
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformGridHandlerScript : MonoBehaviour 
{
    public GameObject ButtonPrefab;
    public Transform ButtonParent;
    private void Start()
    {
        CreateButtons();
    }
    public void CreateButtons()
    {
        List<Texture> buttonsTextures = new List<Texture>(Resources.LoadAll<Texture>("icons"));
        
        for (int i = 0; i < buttonsTextures.Count; i++)
        {
            CreateAnButton(buttonsTextures[i]);
        }
    }
    private void CreateAnButton(Texture texture)
    {
        GameObject newButton = Instantiate(ButtonPrefab, ButtonParent);
        RawImage image = newButton.GetComponent<RawImage>();
        image.texture = texture; 
    }
}
