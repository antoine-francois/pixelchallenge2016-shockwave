using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SetColorLUT : MonoBehaviour
{
    public Texture2D _tTexture;
    public ColorCorrectionLookup _tEffect;

	void Start ()
    {
        _tEffect.Convert(_tTexture, string.Empty);
	}
}
