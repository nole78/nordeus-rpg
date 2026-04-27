using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LineConnector : MonoBehaviour
    {
        public RectTransform line;
        public RectTransform pointA;
        public RectTransform pointB;

        void Start()
        {
            DrawLine();
        }

        void DrawLine()
        {
            Vector3 dir = pointB.position - pointA.position;

            // position
            line.position = (pointA.position + pointB.position) / 2f;

            // length
            line.sizeDelta = new Vector2(dir.magnitude, 5f);

            // rotation
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            line.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
