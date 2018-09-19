using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Game.Animations {
    class MenuAnimations {
        public const float rotationSpeed = 20.0f;

        public static void Rotation (GameObject obj) {
            //obj.transform.Rotate (Vector3.up * Time.deltaTime);
            float random = UnityEngine.Random.Range (-1, 1);
            obj.transform.Rotate (new Vector3 (random * Time.deltaTime * rotationSpeed, random * Time.deltaTime * rotationSpeed, random * Time.deltaTime * rotationSpeed));

        }
    }
}