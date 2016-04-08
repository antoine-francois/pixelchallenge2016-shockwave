using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;


public class LineData
{
    public List<Vector2> _tPosList = new List<Vector2>();
    public Color _tColor;
}

public class DrawLine : MonoBehaviour
{
    public static DrawLine Instance { get; private set; }

	private Material _tLineMaterial;
    private List<LineData> _tData = new List<LineData>();

    void Awake()
    {
        Instance = this;
    }

	void Start()
    {
        Shader tShader = Shader.Find( "Hidden/Internal-Colored" );
		_tLineMaterial = new Material( tShader );
		_tLineMaterial.hideFlags = HideFlags.HideAndDontSave;
		_tLineMaterial.SetInt( "_SrcBlend", (int)BlendMode.SrcAlpha );
		_tLineMaterial.SetInt( "_DstBlend", (int)BlendMode.OneMinusSrcAlpha );
		_tLineMaterial.SetInt( "_Cull", (int)CullMode.Off );
		_tLineMaterial.SetInt( "_ZWrite", 0 );
	}

    public void AddLineData( LineData tLine )
    {
        _tData.Add( tLine );
    }

    public void RemoveLineData( LineData tLine )
    {
        _tData.Remove( tLine );
    }

    public void OnRenderObject()
	{
		_tLineMaterial.SetPass( 0 );

		GL.PushMatrix();
		GL.MultMatrix( transform.localToWorldMatrix );

		GL.Begin( GL.LINES );
        for( int iData = 0; iData < _tData.Count; iData++ )
        {
		    for (int i = 1; i < _tData[iData]._tPosList.Count; ++i)
		    {
			    GL.Color( _tData[iData]._tColor );
			    GL.Vertex3( _tData[iData]._tPosList[i-1].x, _tData[iData]._tPosList[i-1].y, Camera.main.transform.position.y );
			    GL.Vertex3( _tData[iData]._tPosList[i].x, _tData[iData]._tPosList[i].y, Camera.main.transform.position.y );
		    }
        }
		GL.End ();
		GL.PopMatrix ();
	}

    public static List<Vector2> GetCircle( Vector2 tPos, float fRadius, float fAdd )
    {
        List<Vector2> tList = new List<Vector2>();

        for( float f = 0f; f < Mathf.PI * 2.1f; f += fAdd )
        {
            tList.Add( new Vector2( tPos.x + Mathf.Sin( f ) * fRadius, tPos.y + Mathf.Cos( f ) * fRadius ) );
        }
        return tList;
    }
}
