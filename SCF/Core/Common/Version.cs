using System.ComponentModel.DataAnnotations.Schema;

namespace Peyton.Core.Common
{
    [ComplexType]
    public class Version
    {
        public int? No { get; set; }
        public int? Major { get; set; }
        public int? Minor { get; set; }
        public int? Build { get; set; }
        public Version()
        {
            No = 1;
        }
        public Version(int no, int major, int minor, int build)
        {
            No = no;
            Major = major;
            Minor = minor;
            Build = build;
        }
        public override string ToString()
        {
            return string.Format("{1}.{2}.{3}.{4}", No, Major, Minor, Build);
            
        }
    }
}
