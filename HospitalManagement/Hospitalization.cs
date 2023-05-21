using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDJ_HospitalManager
{
    class Hospitalization
    {
        public string ID { get; set; }              // 입원 번호
        public string Hospital_Ward_ID { get; set; }   // 병동 번호
        public string Hospital_Room_ID { get; set; }   // 병실 번호
        public string Patient_ID { get; set; }      // 환자 번호
        public DateTime Join_Date { get; set; }     // 입원 날짜
        public DateTime Exit_Date { get; set; }     // 퇴원 날짜
    }
}
