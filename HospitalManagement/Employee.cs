using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDJ_HospitalManager
{
    class Employee
    {
        public string ID { get; set; }                  // 사원 번호
        public string Name { get; set; }                // 이름
        public DateTime Date_Of_Birth { get; set; }     // 생년월일
        public DateTime Join_Date { get; set; }         // 입사일
        public string Sex { get; set; }                 // 성별
        public string Zip { get; set; }                 // 우편번호
        public string Addr { get; set; }                // 주소
        public string Department_ID { get; set; }                // 부서번호
        public string Position { get; set; }            // 직급
        public string Email { get; set; }               // 이메일
        public string Contact { get; set; }             // 연락처
    }
}
