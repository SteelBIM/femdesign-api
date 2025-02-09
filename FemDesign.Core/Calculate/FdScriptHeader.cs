// https://strusoft.com/
using System.Xml.Serialization;


namespace FemDesign.Calculate
{
    /// <summary>
    /// fdscript.xsd    
    /// FDSCRIPTHEADER
    /// </summary>
    public partial class FdScriptHeader
    {
        [XmlElement("title")]
        public string Title { get; set; } // SZBUF
        [XmlElement("version")]
        public string Version { get; set; } // SZNAME
        [XmlElement("module")]
        public string Module { get; set; } // SZPATH (?)
        [XmlElement("logfile")]
        public string LogFile { get; set; } // SZPATH

        /// <summary>
        /// Parameterless constructor for serialization.
        /// </summary>
        private FdScriptHeader()
        {
            
        }
        public FdScriptHeader(string title, string logfile)
        {
            this.Title = title;
            this.Version = "2100";
            this.Module = "sframe";
            this.LogFile = logfile;
        }
    }
}