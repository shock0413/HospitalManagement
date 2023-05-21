using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDJ_HospitalManager
{
    class Patient
    {
        public string ID { get; set; }                  // 고유번호
        public string Name { get; set; }                // 이름
        public string Sex { get; set; }                 // 성별
        public DateTime Date_Of_Birth { get; set; }     // 생년월일
        public string Zip { get; set; }                 // 우편번호
        public string Addr { get; set; }                // 주소
        public string Contact { get; set; }             // 연락처
        public string Hospital_Ward_ID { get; set; }    // 병동 번호
        public string Hospital_Room_ID { get; set; }    // 병실 번호
    }
}
