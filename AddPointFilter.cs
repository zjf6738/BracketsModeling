
namespace Kitware.VTK
{
    using Kitware.mummy.Runtime;
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    public class AddPointFilter:vtkPolyDataAlgorithm
    {
        //public static readonly string MRClassNameKey = "class AddPointFilter";
        //public const string MRFullTypeName = "Kitware.VTK.AddPointFilter";

        public AddPointFilter()
        {
            Methods.RegisterType(Assembly.GetEntryAssembly(),MRClassNameKey,Type.GetType("Kitware.VTK.AddPointFilter"));
        }
        public AddPointFilter(IntPtr rawCppThis, bool callDisposalMethod, bool strong) : base(rawCppThis, callDisposalMethod, strong)
        {
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        //private

    }
}
