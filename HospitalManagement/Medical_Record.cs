using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDJ_HospitalManager
{
    class Medical_Record
    {
        public string ID { get; set; }          // 진료기록 번호
        public string Patient_ID { get; set; }     // 환자 번호
        public string Employee_ID { get; set; }      // 사원 번호
        public DateTime ReceiptAt { get; set; }        // 접수 날짜
    }
}
