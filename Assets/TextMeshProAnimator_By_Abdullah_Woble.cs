using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextMeshProAnimator_By_Abdullah_Woble : MonoBehaviour
{
    TMP_Text textmesh;

    Mesh mesh;

    Vector3[] vertices;
    public float speed;
    private void Start()
    {
        textmesh = GetComponent<TMP_Text>();
    }


    private void Update()
    {
        textmesh.ForceMeshUpdate();
        mesh = textmesh.mesh;
        vertices = mesh.vertices;

        for (int i = 0; i < textmesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textmesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            Vector3 offset = Woble(Time.time * i );

            vertices[index] += offset;
            vertices[index + 1] += offset;
            vertices[index + 2] += offset;
            vertices[index + 3] += offset;

        }

        mesh.vertices = vertices;
        textmesh.canvasRenderer.SetMesh(mesh);

    }


    Vector2 Woble(float time)
    {
        return new Vector2(Mathf.Sin(time * 2f), Mathf.Cos(time * 1f));
    }
}
