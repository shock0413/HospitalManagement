using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDJ_HospitalManager
{
    class Department
    {
        public string ID { get; set; }                      // 고유번호
        public string Name { get; set; }                    // 부서명
        public string Is_Medical_Department { get; set; }   // 진료과목여부
    }
}