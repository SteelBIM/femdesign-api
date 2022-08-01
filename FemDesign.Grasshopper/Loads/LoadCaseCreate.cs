// https://strusoft.com/
using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using Rhino.Geometry;
using FemDesign.Loads;

namespace FemDesign.Grasshopper
{
    public class LoadCaseCreate: GH_Component
    {
        public LoadCaseCreate(): base("LoadCase.Create", "Create", "Creates a load case.", "FEM-Design", "Loads")
        {

        }
        protected override void RegisterInputParams(GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Name", "Name", "Name of LoadCase.", GH_ParamAccess.item);
            pManager.AddTextParameter("Type", "Type", "Connect 'ValueList' to get the options.\nLoadCase type:\nordinary\ndead_load\nsoil_dead_load\nshrinkage\nprestressing\nfire\nseis_sxp\nseis_sxm\nseis_syp\nseis_sym.", GH_ParamAccess.item, "Ordinary");
            pManager[pManager.ParamCount - 1].Optional = true;
            pManager.AddTextParameter("DurationClass", "DurationClass", "Connect 'ValueList' to get the options.\nLoadCase duration class:\npermanent\nlong-term\nmedium-term\nshort-term\ninstantaneous.", GH_ParamAccess.item, "permanent");
            pManager[pManager.ParamCount - 1].Optional = true;
        }
        protected override void RegisterOutputParams(GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("LoadCase", "LoadCase", "LoadCase.", GH_ParamAccess.item);
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // get data
            string name = null, type = "Ordinary", durationClass = "permanent";
            if (!DA.GetData(0, ref name)) { return; }
            if (!DA.GetData(1, ref type))
            {
                // pass
            }

            var resultTypes = new List<string>() { "ordinary", "dead_load", "soil_dead_load", "shrinkage", "prestressing", "fire", "seis_sxp", "seis_sxm", "seis_syp", "seis_sym" };
            FemDesign.Grasshopper.Extension.ComponentExtensions.SetValueList(this, resultTypes, 1);


            if (!DA.GetData(2, ref durationClass))
            {
                // pass
            }
            resultTypes = new List<string>() { "permanent", "long-term", "medium-term", "short-term", "instantaneous" };
            FemDesign.Grasshopper.Extension.ComponentExtensions.SetValueList(this, resultTypes, 2);

            LoadCaseType _type = FemDesign.GenericClasses.EnumParser.Parse<LoadCaseType>(type);
            LoadCaseDuration _durationClass = FemDesign.GenericClasses.EnumParser.Parse<LoadCaseDuration>(durationClass);
            FemDesign.Loads.LoadCase obj = new FemDesign.Loads.LoadCase(name, _type, _durationClass);

            // return
            DA.SetData(0, obj);
        }
        protected override System.Drawing.Bitmap Icon
        {
            get
            {

                return FemDesign.Properties.Resources.LoadCaseDefine;
            }
        }
        public override Guid ComponentGuid
        {
            get { return new Guid("ebf804c1-91a6-40bb-adee-5a02a9e42f80"); }
        }

        public override GH_Exposure Exposure => GH_Exposure.quarternary;

        //private void SetValueList(List<string> ResultTypes, int inputIndex)
        //{

        //    if (this.Params.Input[inputIndex].SourceCount == 1)
        //    {
        //        if (this.Params.Input[inputIndex]?.Sources[0] is GH_ValueList)
        //        {
        //            var valueList = Params.Input[inputIndex].Sources[0] as GH_ValueList;
        //            valueList.ListMode = GH_ValueListMode.DropDown;

        //            if (valueList.ListItems.Count != ResultTypes.Count)
        //            {
        //                valueList.ListItems.Clear();
        //                for (int i = 0; i < ResultTypes.Count; i++)
        //                {
        //                    var name = ResultTypes[i];
        //                    valueList.ListItems.Add(new GH_ValueListItem(name, String.Format("\"{0}\"", name)));
        //                }
        //            }
        //            valueList.SelectItem(1);
        //            valueList.Attributes.ExpireLayout();
        //            valueList.Attributes.PerformLayout();
        //        }
        //    }
        //}

    }
}