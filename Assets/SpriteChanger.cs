using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

	public Texture2D characterTexture2D;

	// Use this for initialization
	void Start () {
		// UpdateCharacterTexture();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//CopiedTexture is the original Texture  which you want to copy.
public Texture2D CopyTexture2D(Texture2D copiedTexture)
{
	//Create a new Texture2D, which will be the copy.
	Texture2D texture = new Texture2D(copiedTexture.width, copiedTexture.height);
	//Choose your filtermode and wrapmode here.
	texture.filterMode = FilterMode.Point;
	texture.wrapMode = TextureWrapMode.Clamp;

	int y = 0;
	while (y < texture.height)
	{
		int x = 0;
		while (x < texture.width)
		{
			if(copiedTexture.GetPixel(x,y) == Color.black)
			{
				texture.SetPixel(x, y, Color.white);
			} else {
				texture.SetPixel(x, y, copiedTexture.GetPixel(x,y));
			}
			++x;
		}
		++y;
	}
	//Name the texture, if you want.
	// texture.name = (Species+Gender+"_SpriteSheet");

	texture.Apply();

	return texture;
}
   
    public void UpdateCharacterTexture()
    {
//This calls the copy texture function, and copies it. The variable characterTextures2D is a Texture2D which is now the returned newly copied Texture2D.
        characterTexture2D = CopyTexture2D(gameObject.GetComponent<SpriteRenderer> ().sprite.texture);
   
		//Get your SpriteRenderer, get the name of the old sprite,  create a new sprite, name the sprite the old name, and then update the material. If you have multiple sprites, you will want to do this in a loop- which I will post later in another post.
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        string tempName = sr.sprite.name;
        sr.sprite = Sprite.Create (characterTexture2D, sr.sprite.rect, new Vector2(0,1));
        sr.sprite.name = tempName;
 
        sr.material.mainTexture = characterTexture2D;
        sr.material.shader = Shader.Find ("Sprites/Transparent Unlit");
 
    }
}
