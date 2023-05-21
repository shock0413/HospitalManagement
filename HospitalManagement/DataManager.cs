using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KDJ_HospitalManager
{
    class DataManager
    {
        public static List<Doctor> Doctors = new List<Doctor>();
        public static List<Patient> Patients = new List<Patient>();
        public static List<Department> Departments = new List<Department>();
        public static List<Employee> Employees = new List<Employee>();
        public static List<Hospital_Ward> Hospital_Wards = new List<Hospital_Ward>();
        public static List<Hospital_Room> Hospital_Rooms = new List<Hospital_Room>();
        public static List<Medical_Record> Medical_Records = new List<Medical_Record>();

        public static int patient_id = 1;
        public static int employee_id = 1;
        public static int department_id = 1;
        public static int ward_id = 1;
        public static int room_id = 1;
        public static int record_id = 1;

        static DataManager()
        {
            Load();
        }

        public static void Load()
        {
            try
            {
                string patientsInput = File.ReadAllText(@"./patients.xml");
                XElement patientsXElement = XElement.Parse(patientsInput);
                Patients = (from patient in patientsXElement.Descendants("patient")
                            select new Patient()
                            {
                                ID = patient.Element("id").Value,
                                Name = patient.Element("name").Value,
                                Sex = patient.Element("sex").Value,
                                Date_Of_Birth = DateTime.Parse(patient.Element("date_of_birth").Value),
                                Zip = patient.Element("zip").Value,
                                Addr = patient.Element("addr").Value,
                                Contact = patient.Element("contact").Value,
                                Hospital_Ward_ID = patient.Element("hospital_ward_id").Value,
                                Hospital_Room_ID = patient.Element("hospital_room_id").Value
                            }).ToList<Patient>();

                string employeesInput = File.ReadAllText(@"./employees.xml");
                XElement employeesXElement = XElement.Parse(employeesInput);
                Employees = (from employee in employeesXElement.Descendants("employee")
                             select new Employee()
                             {
                                 ID = employee.Element("id").Value,
                                 Name = employee.Element("name").Value,
                                 Sex = employee.Element("sex").Value,
                                 Date_Of_Birth = DateTime.Parse(employee.Element("date_of_birth").Value).Date,
                                 Join_Date = DateTime.Parse(employee.Element("join_date").Value).Date,
                                 Zip = employee.Element("zip").Value,
                                 Addr = employee.Element("addr").Value,
                                 Department_ID = employee.Element("department").Value,
                                 Position = employee.Element("position").Value,
                                 Email = employee.Element("email").Value
                             }).ToList<Employee>();

                string departmentsInput = File.ReadAllText(@"./departments.xml");
                XElement departmentsXElement = XElement.Parse(departmentsInput);
                Departments = (from department in departmentsXElement.Descendants("department")
                               select new Department()
                               {
                                   ID = department.Element("id").Value,
                                   Name = department.Element("name").Value,
                                   Is_Medical_Department = department.Element("is_medical_department").Value
                               }).ToList<Department>();

                string hospital_wardsInput = File.ReadAllText(@"./hospital_wards.xml");
                XElement hospital_wardsXElement = XElement.Parse(hospital_wardsInput);
                Hospital_Wards = (from ward in hospital_wardsXElement.Descendants("hospital_ward")
                                  select new Hospital_Ward()
                                  {
                                      ID = ward.Element("id").Value,
                                      Name = ward.Element("name").Value,
                                      Count = ward.Element("count").Value
                                  }).ToList<Hospital_Ward>();

                string hospital_roomsInput = File.ReadAllText(@"./hospital_rooms.xml");
                XElement hospital_roomsXElement = XElement.Parse(hospital_roomsInput);
                Hospital_Rooms = (from room in hospital_roomsXElement.Descendants("hospital_room")
                                  select new Hospital_Room()
                                  {
                                      ID = room.Element("id").Value,
                                      Number = room.Element("number").Value,
                                      Hospital_Ward_ID = room.Element("hospital_ward_id").Value,
                                      Count = room.Element("count").Value
                                  }).ToList<Hospital_Room>();

                string medical_recordsInput = File.ReadAllText(@"./medical_records.xml");
                XElement medical_recordsXElement = XElement.Parse(medical_recordsInput);
                Medical_Records = (from record in medical_recordsXElement.Descendants("medical_record")
                                   select new Medical_Record()
                                   {
                                       ID = record.Element("id").Value,
                                       Patient_ID = record.Element("patient_id").Value,
                                       Employee_ID = record.Element("employee_id").Value,
                                       ReceiptAt = DateTime.Parse(record.Element("receiptAt").Value)
                                   }).ToList<Medical_Record>();
            }
            catch (FileNotFoundException e)
            {
                Save();
            }

            if (Patients.Count() != 0)
                patient_id = int.Parse(Patients[Patients.Count() - 1].ID) + 1;
            if (Employees.Count() != 0)
                employee_id = int.Parse(Employees[Employees.Count() - 1].ID) + 1;
            if (Departments.Count() != 0)
                department_id = int.Parse(Departments[Departments.Count() - 1].ID) + 1;
            if (Hospital_Wards.Count() != 0)
                ward_id = int.Parse(Hospital_Wards[Hospital_Wards.Count() - 1].ID) + 1;
            if (Hospital_Rooms.Count() != 0)
                room_id = int.Parse(Hospital_Rooms[Hospital_Rooms.Count() - 1].ID) + 1;
            if (Medical_Records.Count() != 0)
                record_id = int.Parse(Medical_Records[Medical_Records.Count() - 1].ID) + 1;
        }

        public static void Save()
        {
            string patientsOutput = "<patients>\n";
            foreach (var patient in Patients)
            {
                patientsOutput += " <patient>\n";
                patientsOutput += "     <id>" + patient.ID + "</id>\n";
                patientsOutput += "     <name>" + patient.Name + "</name>\n";
                patientsOutput += "     <sex>" + patient.Sex + "</sex>\n";
                patientsOutput += "     <date_of_birth>" + patient.Date_Of_Birth.Date.ToString() + "</date_of_birth>\n";
                patientsOutput += "     <zip>" + patient.Zip + "</zip>\n";
                patientsOutput += "     <addr>" + patient.Addr + "</addr>\n";
                patientsOutput += "     <contact>" + patient.Contact + "</contact>\n";
                patientsOutput += "     <hospital_ward_id>" + patient.Hospital_Ward_ID + "</hospital_ward_id>\n";
                patientsOutput += "     <hospital_room_id>" + patient.Hospital_Room_ID + "</hospital_room_id>\n";
                patientsOutput += " </patient>\n";
            }
            patientsOutput += "</patients>";

            string employeesOutput = "<employees>\n";
            foreach (var employee in Employees)
            {
                employeesOutput += " <employee>\n";
                employeesOutput += "     <id>" + employee.ID + "</id>\n";
                employeesOutput += "     <name>" + employee.Name + "</name>\n";
                employeesOutput += "     <sex>" + employee.Sex + "</sex>\n";
                employeesOutput += "     <date_of_birth>" + employee.Date_Of_Birth.Date + "</date_of_birth>\n";
                employeesOutput += "     <join_date>" + employee.Join_Date + "</join_date>\n";
                employeesOutput += "     <zip>" + employee.Zip + "</zip>\n";
                employeesOutput += "     <addr>" + employee.Addr + "</addr>\n";
                employeesOutput += "     <department>" + employee.Department_ID + "</department>\n";
                employeesOutput += "     <position>" + employee.Position + "</position>\n";
                employeesOutput += "     <email>" + employee.Email + "</email>\n";
                employeesOutput += " </employee>\n";
            }
            employeesOutput += "</employees>";

            string departmentsOutput = "<departments>\n";
            foreach (var department in Departments)
            {
                departmentsOutput += "  <department>\n";
                departmentsOutput += "      <id>" + department.ID + "</id>\n";
                departmentsOutput += "      <name>" + department.Name + "</name>\n";
                departmentsOutput += "      <is_medical_department>" + department.Is_Medical_Department + "</is_medical_department>\n";
                departmentsOutput += "  </department>\n";
            }
            departmentsOutput += "</departments>";

            string hospital_wardsOutput = "<hospital_wards>\n";
            foreach (var ward in Hospital_Wards)
            {
                hospital_wardsOutput += "   <hospital_ward>\n";
                hospital_wardsOutput += "       <id>" + ward.ID + "</id>\n";
                hospital_wardsOutput += "       <name>" + ward.Name + "</name>\n";
                hospital_wardsOutput += "       <count>" + ward.Count + "</count>\n";
                hospital_wardsOutput += "   </hospital_ward>\n";
            }
            hospital_wardsOutput += "</hospital_wards>";

            string hospital_roomsOutput = "<hospital_rooms>\n";
            foreach (var room in Hospital_Rooms)
            {
                hospital_roomsOutput += "   <hospital_room>\n";
                hospital_roomsOutput += "       <id>" + room.ID + "</id>\n";
                hospital_roomsOutput += "       <number>" + room.Number + "</number>\n";
                hospital_roomsOutput += "       <hospital_ward_id>" + room.Hospital_Ward_ID + "</hospital_ward_id>\n";
                hospital_roomsOutput += "       <count>" + room.Count + "</count>\n";
                hospital_roomsOutput += "   </hospital_room>\n";
            }
            hospital_roomsOutput += "</hospital_rooms>";

            string medical_recordsOutput = "<medical_records>\n";
            foreach (var record in Medical_Records)
            {
                medical_recordsOutput += "  <medical_record>\n";
                medical_recordsOutput += "      <id>" + record.ID + "</id>\n";
                medical_recordsOutput += "      <patient_id>" + record.Patient_ID + "</patient_id>\n";
                medical_recordsOutput += "      <employee_id>" + record.Employee_ID + "</employee_id>\n";
                medical_recordsOutput += "      <receiptAt>" + record.ReceiptAt + "</receiptAt>\n";
                medical_recordsOutput += "  </medical_record>\n";
            }
            medical_recordsOutput += "</medical_records>";

            try
            {
                File.WriteAllText(@"./patients.xml", patientsOutput);
                File.WriteAllText(@"./employees.xml", employeesOutput);
                File.WriteAllText(@"./departments.xml", departmentsOutput);
                File.WriteAllText(@"./hospital_wards.xml", hospital_wardsOutput);
                File.WriteAllText(@"./hospital_rooms.xml", hospital_roomsOutput);
                File.WriteAllText(@"./medical_records.xml", medical_recordsOutput);
            }
            catch (Exception e)
            {

            }
        }
    }
}