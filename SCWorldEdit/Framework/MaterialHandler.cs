using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SCWorldEdit.Framework
{
    public class MaterialHandler : IMaterialHandler
    {
        public DiffuseMaterial[] MaterialInventory { get; set; }

        public MaterialHandler()
        {
            //TODO: Pull the BlocksData.xml and load the brushes and materials from that.

            MaterialInventory = new DiffuseMaterial[203];

            DiffuseMaterial localMaterial = new DiffuseMaterial(Brushes.Red);

            MaterialInventory[3] = localMaterial;
        }
    }
}
