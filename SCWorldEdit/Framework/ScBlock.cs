﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using ServiceLocator;

namespace SCWorldEdit.Framework
{
    public class ScBlock 
    {
        public GeometryModel3D BlockModel { get; set; }

        public Byte BlockType;
        public Byte BlockData;
        
        public ScBlock(Point3D argPosition, Byte argBlockType, Byte argBlockData)
        {
            var localMaterialHandler = Locator.Resolve<IMaterialHandler>();

            BlockType = argBlockType;
            BlockData = argBlockData;

            #region ** Mesh Posittions **

            var localMesh = new MeshGeometry3D();

			//????
			//Blobk position is Chunk location *16 + block index (how far into the chunk is the block)
			//????
            //Positions="0,0,0  1,0,0  0,1,0  1,1,0  0,0,-1  1,0,-1  0,1,-1  1,1,-1 "
            localMesh.Positions.Add(argPosition);                                                          // 0, 0,  0
            localMesh.Positions.Add(new Point3D(argPosition.X + 1, argPosition.Y, argPosition.Z));         // 1, 0,  0
            localMesh.Positions.Add(new Point3D(argPosition.X, argPosition.Y + 1, argPosition.Z));         // 0, 1,  0
            localMesh.Positions.Add(new Point3D(argPosition.X + 1, argPosition.Y + 1, argPosition.Z));     // 1, 1,  0
            localMesh.Positions.Add(new Point3D(argPosition.X, argPosition.Y, argPosition.Z - 1));         // 0, 0, -1
            localMesh.Positions.Add(new Point3D(argPosition.X + 1, argPosition.Y, argPosition.Z - 1));     // 1, 0, -1
            localMesh.Positions.Add(new Point3D(argPosition.X, argPosition.Y + 1, argPosition.Z - 1));     // 0, 1, -1
            localMesh.Positions.Add(new Point3D(argPosition.X + 1, argPosition.Y + 1, argPosition.Z - 1)); // 1, 1, -1

            #endregion

            #region ** Triangle Indicies **

            //TriangleIndices = "0 1 2 3 2 1   3 1 5 3 5 7   0 6 4 0 2 6   2 3 6 3 7 6   0 4 5 0 5 1   4 7 5 4 6 7 "
            var triangleIndices = new Int32Collection {
                                                        0, 1 ,2 ,3, 2, 1,  //Front
                                                        3, 1, 5, 3, 5, 7,  //Right
                                                        0, 6, 4, 0, 2, 6,  //Left
                                                        2, 3, 6, 3, 7, 6,  //Top
                                                        0, 4, 5, 0, 5, 1,  //Bottom
                                                        4, 7, 5, 4, 6, 7,  //Back
                                                    };

            localMesh.TriangleIndices = triangleIndices;

            #endregion

            BlockModel = new GeometryModel3D();

            BlockModel.Geometry = localMesh;
            BlockModel.Material = localMaterialHandler.MaterialInventory[3];

        }

    }
}
