using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_4
{
    class Patient
    {
        public string OHIP_Number { get; set; }
        public string OHIP_Code { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        //public string Valid_Gender { get; set; }
        public string DOB { get; set; }
        public string Error { get; set; }

        public Patient(object OHIP_Num, object OHIP_Cod)
        {
            this.OHIP_Number = (string) OHIP_Num;
            this.OHIP_Code = OHIP_Cod.ToString();
        }

    }
}
