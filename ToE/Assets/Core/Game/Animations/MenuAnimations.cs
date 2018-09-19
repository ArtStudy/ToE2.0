﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Core.Game.Animations {
    class MenuAnimations {
        public const float rotationSpeed = 10.0f;

        public static void Rotation (GameObject obj) {
            //obj.transform.Rotate (Vector3.up * Time.deltaTime);
            obj.transform.Rotate (new Vector3 (UnityEngine.Random.Range (-1, 1), UnityEngine.Random.Range (-1, 1), UnityEngine.Random.Range (-1, 1)) * Time.deltaTime);

        }
    }
}