using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace SCWorldEdit.Framework
{
    public interface IMaterialHandler
    {
        DiffuseMaterial[] MaterialInventory { get; set; }
    }
}