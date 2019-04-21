using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[AddComponentMenu("NGUI/UI/Flyword")]
public class UIFlyword : MonoBehaviour
{
    public enum HorizontalAlignType
    {
        Left,
        Center,
        Right
    }

    [SerializeField]
    public UIAtlas atlas;
    [HideInInspector]
    public string path;
    [HideInInspector]
    public EFlyWordType flywordType = EFlyWordType.TYPE_AVATAR_HURT;
    [HideInInspector]
    public HorizontalAlignType hAlignType = HorizontalAlignType.Center;
    [HideInInspector]
    public string text = string.Empty;
    [HideInInspector]
    public Color color = Color.white;
    [HideInInspector]
    public Color outlineColor = Color.white;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public AnimationCurve moveCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve alphaCurve;

    private float mAlpha = 1;
    private float mScale = 1f;
    private float mLifeTime = 1;
    private float mStartTime;
    private bool  mIsPlaying = false;
    public Vector3  startPos = Vector3.zero;
    public float    startSize = 1f;
    public float    offset = 300;

    public void Init(Vector3 pos,float size)
    {
        //transform.position = pos;
        //transform.localScale = new Vector3(20, 20, 20);
        startPos = pos;
        startSize = size;
        transform.position = pos;
        mStartTime = Time.realtimeSinceStartup;
        Invoke("Release", mLifeTime);
        mIsPlaying = true;
    }

    void Start()
    {
        meshRenderer = transform.FindChild("Mesh").GetComponent<MeshRenderer>();
        meshFilter = transform.FindChild("Mesh").GetComponent<MeshFilter>();
        meshRenderer.material = atlas.spriteMaterial;
        if (!string.IsNullOrEmpty(text))
        {
            GenerateFilter();
        }
    }

    void Update()
    {
        if(mIsPlaying==false)
        {
            return;
        }
        Keyframe[] offsets = moveCurve.keys;
        Keyframe[] alphas = alphaCurve.keys;
        Keyframe[] scales = scaleCurve.keys;
        float time = Time.realtimeSinceStartup;
        float offsetEnd = offsets[offsets.Length - 1].time;
        float alphaEnd = alphas[alphas.Length - 1].time;
        float scalesEnd = scales[scales.Length - 1].time;
        float totalEnd = Mathf.Max(scalesEnd, Mathf.Max(offsetEnd, alphaEnd));
        float currentTime = time - mStartTime;
        float o = moveCurve.Evaluate(currentTime);
        float a = alphaCurve.Evaluate(currentTime);
        float s = scaleCurve.Evaluate(currentTime);
        //SetAlpha(o);
        SetScale(s);
        transform.position = startPos + new Vector3(0,o*offset,0);
    }

    void Release()
    {
        ZTPool.Instance.ReleaseGo(path, gameObject, EPoolObjectType.PMesh);
        mIsPlaying = false;
    }

    void OnDestroy()
    {
        CancelInvoke();
    }

    public void SetColor(Color col, Color outlineCol)
    {
        color = col;
        outlineColor = outlineCol;
    }

    public void SetColor(Color col)
    {
        color = col;
        outlineColor = col;
    }

    public void SetScale(float scale)
    {
        mScale = scale * startSize;
        transform.localScale = Vector3.one * mScale;
    }

    public void SetAlpha(float alpha)
    {
        mAlpha = alpha;
        color.a = alpha;
        outlineColor.a = alpha;
    }

    public void Show(string text)
    {
        this.text = text;
        GenerateFilter();
    }

    public void GenerateFilter()
    {
        Mesh mesh = new Mesh();

        int length = text.Length;
        Vector3[] vertices = new Vector3[length << 2];
        Vector2[] uvs = new Vector2[vertices.Length];
        int[] triangles = new int[(length << 1) * 3];
        Texture tex = atlas.texture;
        Color[] colors = new Color[vertices.Length];
        int tmp = 0;
        float tmp2 = 0;
        switch (hAlignType)
        {
            case HorizontalAlignType.Center:
                tmp2 = -(vertices.Length >> 3);
                break;
            case HorizontalAlignType.Left:
                tmp2 = 0;
                break;
            case HorizontalAlignType.Right:
                tmp2 = -(vertices.Length >> 2);
                break;
            default:
                tmp2 = 0;
                break;
        }
        float r = 1;
        for (int i = 0; i < vertices.Length; i += 4)
        {
            tmp = (i + 1) % 2;

            string s = text[i / 4].ToString();
            UISpriteData mSprite = atlas.GetSprite(s);
            r = (mSprite.width * 1.0f / mSprite.height);
            vertices[i] = new Vector3(tmp2, tmp + 1);
            vertices[i + 1] = new Vector3(tmp2, tmp);
            tmp2 += r;
            vertices[i + 2] = new Vector3(tmp2, tmp + 1);
            vertices[i + 3] = new Vector3(tmp2, tmp);


            colors[i] = color;
            colors[i + 1] = outlineColor;
            colors[i + 2] = color;
            colors[i + 3] = outlineColor;

            Rect inner = new Rect(mSprite.x + mSprite.borderLeft, mSprite.y + mSprite.borderTop,
                mSprite.width - mSprite.borderLeft - mSprite.borderRight,
                mSprite.height - mSprite.borderBottom - mSprite.borderTop);
            inner = NGUIMath.ConvertToTexCoords(inner, tex.width, tex.height);

            uvs[i] = new Vector2(inner.xMin, inner.yMax);
            uvs[i + 1] = new Vector2(inner.xMin, inner.yMin);
            uvs[i + 2] = new Vector2(inner.xMax, inner.yMax);
            uvs[i + 3] = new Vector2(inner.xMax, inner.yMin);
        }

        for (int i = 0; i < triangles.Length; i += 6)
        {
            tmp = (i / 3) << 1;
            triangles[i] = triangles[i + 3] = tmp;
            triangles[i + 1] = triangles[i + 5] = tmp + 3;
            triangles[i + 2] = tmp + 1;
            triangles[i + 4] = tmp + 2;
        }
        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        meshFilter.mesh = mesh;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        DrawMesh();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        DrawMesh();
    }

    void DrawMesh()
    {
        if (meshFilter == null)
        {
            return;
        }
        Mesh mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            return;
        }
        int[] tris = mesh.triangles;
        for (int i = 0; i < tris.Length; i += 3)
        {
            Gizmos.DrawLine(Convert2World(mesh.vertices[tris[i]]), Convert2World(mesh.vertices[tris[i + 1]]));
            Gizmos.DrawLine(Convert2World(mesh.vertices[tris[i]]), Convert2World(mesh.vertices[tris[i + 2]]));
            Gizmos.DrawLine(Convert2World(mesh.vertices[tris[i + 1]]), Convert2World(mesh.vertices[tris[i + 2]]));
        }
    }

    Vector3 Convert2World(Vector3 src)
    {
        return transform.TransformPoint(src);
    }
}
