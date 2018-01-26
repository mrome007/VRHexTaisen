using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour 
{
    public List<HexTile> AdjacentTiles;

    private Collider hexTileCollider;

    private Material hexTileMaterial;

    private Color highlightColor = new Color(0.5f, 0.5f, 0.8f, 1f);
    private Color normalColor = Color.white;

    private void Awake()
    {
        AdjacentTiles = new List<HexTile>();
        hexTileCollider = GetComponent<Collider>();
        hexTileMaterial = transform.GetChild(0).GetComponent<Renderer>().material;
    }

    public void EnableHexCollider(bool enable)
    {
        hexTileCollider.enabled = enable;
    }

    public void HighlightHexTile(bool highlight)
    {
        hexTileMaterial.color = highlight ? highlightColor : normalColor;
    }
}
