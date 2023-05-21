using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KDJ_HospitalManager
{
    class Hospital_Room
    {
        public string ID { get; set; }                  // 병실 번호
        public string Number { get; set; }              // 병실 호실
        public string Hospital_Ward_ID { get; set; }    // 병동 번호
        public string Count { get; set; }               // 인원 수
    }
}
