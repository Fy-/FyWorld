/*
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
| FyWorld - A top down simulation game in a fantasy medieval world.    |
|                                                                      |
|    :copyright: © 2019 Florian Gasquez.                               |
|    :license: GPLv3, see LICENSE for more details.                    |
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
*/
using System.Collections.Generic;
using UnityEngine;
using Fy.World;
using Fy.Definitions;
using Fy.Entity;
using Fy.Helpers;

namespace Fy.Visuals {
    // Renderer the ground for a region
    // Please check my tutorial at: https://fyworld.net/gamedev/unity-tutorial-tile-blending-edge-smoothing/
    // Video version: https://www.youtube.com/watch?v=1z6i14IOV8Q&list=PL3zJehMxBSE82elgeomlaixuSXcJUm0gO
    public class BucketGroundRenderer : BucketRenderer  {
        public BucketGroundRenderer(LayerGridBucket bucket, Layer layer) : base(bucket, layer) {}

        public override void BuildMeshes() {
            List<int> neighboursGraphicsList = new List<int>();
            int[] neighboursGraphics = new int[8];
            Color[] colors = new Color[9];

            foreach (Tilable ground in this.bucket.tilables) {
                neighboursGraphicsList.Clear();

                MeshData currentMesh = this.GetMesh(ground.mainGraphic.uid, false, (MeshFlags.Base | MeshFlags.Color));
                int vIndex = currentMesh.vertices.Count;
                float z = ground.mainGraphic.priority;

                currentMesh.vertices.Add(new Vector3(ground.position.x, ground.position.y, z));
                currentMesh.vertices.Add(new Vector3(ground.position.x, ground.position.y+1, z));
                currentMesh.vertices.Add(new Vector3(ground.position.x+1, ground.position.y+1, z));
                currentMesh.vertices.Add(new Vector3(ground.position.x+1, ground.position.y, z));
                currentMesh.colors.Add(Color.white);
                currentMesh.colors.Add(Color.white);
                currentMesh.colors.Add(Color.white);
                currentMesh.colors.Add(Color.white);
                currentMesh.AddTriangle(vIndex, 0, 1, 2);
                currentMesh.AddTriangle(vIndex, 0, 2, 3);

                for (int i = 0; i < DirectionUtils.neighbours.Length; i++) {
                    Tilable neighbourGround = Loki.map.groundGrid.GetTilableAt(ground.position + DirectionUtils.neighbours[i]);

                    if (neighbourGround != null) {
                        neighboursGraphics[i] = neighbourGround.mainGraphic.uid;
                        if (
                            !neighboursGraphicsList.Contains(neighbourGround.mainGraphic.uid) &&
                            neighbourGround.mainGraphic.uid != ground.mainGraphic.uid &&
                            neighbourGround.def.groundDef.maxHeight >= ground.def.groundDef.maxHeight
                        ) {
                            neighboursGraphicsList.Add(neighbourGround.mainGraphic.uid);
                        }
                    } else {
                        neighboursGraphics[i] = ground.mainGraphic.uid;
                    }
                }

                foreach (int graphicUID in neighboursGraphicsList) {
                    currentMesh = this.GetMesh(graphicUID, false, (MeshFlags.Base | MeshFlags.Color));
                    vIndex = currentMesh.vertices.Count;
                    z = GraphicInstance.instances[graphicUID].priority;
                    
                    currentMesh.vertices.Add(new Vector3(ground.position.x + .5f, ground.position.y, z));         // 0
                    currentMesh.vertices.Add(new Vector3(ground.position.x, ground.position.y, z));               // 1
                    currentMesh.vertices.Add(new Vector3(ground.position.x, ground.position.y + .5f, z));         // 2
                    currentMesh.vertices.Add(new Vector3(ground.position.x, ground.position.y + 1, z));           // 3
                    currentMesh.vertices.Add(new Vector3(ground.position.x + .5f, ground.position.y + 1, z));     // 4
                    currentMesh.vertices.Add(new Vector3(ground.position.x + 1, ground.position.y + 1, z));       // 5
                    currentMesh.vertices.Add(new Vector3(ground.position.x + 1, ground.position.y + .5f, z));     // 6
                    currentMesh.vertices.Add(new Vector3(ground.position.x + 1, ground.position.y, z));           // 7
                    currentMesh.vertices.Add(new Vector3(ground.position.x + .5f, ground.position.y + .5f, z));   // 8

                    for (int i = 0; i < colors.Length; i++) {
                        colors[i] = Color.clear;
                    }

                    for (int i = 0; i < 8; i++) {
                        if (i % 2 != 0) { // if it's odd
                            if (graphicUID == neighboursGraphics[i]) {
                                colors[i] = Color.white;
                            }
                        } else {
                            if (graphicUID == neighboursGraphics[i]) {
                              switch (i)
                                {
                                    case 0: // South
                                        colors[1] = Color.white;
                                        colors[0] = Color.white;
                                        colors[7] = Color.white;
                                        break;
                                    case 2:  // West
                                        colors[1] = Color.white;
                                        colors[2] = Color.white;
                                        colors[3] = Color.white;
                                        break;
                                    case 4: // North
                                        colors[3] = Color.white;
                                        colors[4] = Color.white;
                                        colors[5] = Color.white;
                                        break;
                                    case 6: // East
                                        colors[5] = Color.white;
                                        colors[6] = Color.white;
                                        colors[7] = Color.white;
                                        break;
                                }
                            }
                        }
                    }

                    currentMesh.colors.AddRange(colors);
                    currentMesh.AddTriangle(vIndex, 0, 8, 6);
                    currentMesh.AddTriangle(vIndex, 0, 6, 7);
                    currentMesh.AddTriangle(vIndex, 1, 8, 0);
                    currentMesh.AddTriangle(vIndex, 1, 2, 8);
                    currentMesh.AddTriangle(vIndex, 2, 4, 8);
                    currentMesh.AddTriangle(vIndex, 2, 3, 4);
                    currentMesh.AddTriangle(vIndex, 8, 5, 6);
                    currentMesh.AddTriangle(vIndex, 8, 4, 5);
                }
            }

            foreach (MeshData meshData in this.meshes.Values) {
                meshData.Build();
            }
        }
    }
}