using NaughtyAttributes;
using UnityEngine;

public class RoundedButton : MonoBehaviour {
    public float width;
    public float height;
    public float borderRadius;

    [Button]
    public void GenerateMesh() {
        var w = width * .5f;
        var h = height * .5f;


        var uv = new Vector2[91 * 4];
        var vertices = new Vector3[91 * 4];

        var j = 0;
        for (var startAngle = 0; startAngle < 360; startAngle += 90) {
            var p = new Vector3((w - borderRadius) * (startAngle == 0 || startAngle == 270 ? 1 : -1),
                (h - borderRadius) * (startAngle < 180 ? 1 : -1));
            for (var i = startAngle; i <= startAngle + 90; i++) {
                var a = i * Mathf.Deg2Rad;
                var pos = p + new Vector3(Mathf.Cos(a), Mathf.Sin(a)) * borderRadius;
                vertices[j] = pos;
                uv[j] = new Vector2(pos.x / width + .5f, pos.y / height + .5f);
                j++;
            }
        }

        var triangles = new int[90 * 3 * 4 + 18];

        for (var o = 0; o < 4; o++) {
            var offset = o * 90;
            var aoff = o * 91;
            triangles[offset * 3] = aoff;
            triangles[1 + offset * 3] = 90 + aoff;
            triangles[2 + offset * 3] = 89 + aoff;


            for (var i = 3; i < 90 * 3; i += 3) {
                triangles[i + offset * 3] = aoff;
                triangles[i + 1 + offset * 3] = triangles[i - 1 + offset * 3];
                triangles[i + 2 + offset * 3] = triangles[i - 1 + offset * 3] - 1;
            }
        }

        var remaining = new[] {
            0, 91, 90,
            91, 182, 181,
            182, 273, 272,
            273, 0, 363,
            273, 91, 0,
            273, 182, 91
        };

        for (var i = 0; i < 18; i++) triangles[90 * 3 * 4 + i] = remaining[i];


        GetComponent<MeshFilter>().mesh = new Mesh {vertices = vertices, triangles = triangles, uv = uv};
    }
}