using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Permissions;

namespace WCFReportingservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Reportingservice" in both code and config file together.
    [ServiceBehavior(IgnoreExtensionDataObject = true, IncludeExceptionDetailInFaults = true)]
    public class Reportingservice : IPublicReportingservice, IPrivateReportingservice, IWCFEmployeeservice, IFileHandlingservice, IWCFRESTEmployeeservice
    {
        //General service test
        //[System.Security.Permissions.PrincipalPermission(SecurityAction.Demand, Role = "ADMIN")]// This is to implement Custom Authorization Policy
        public string Getpublicinformation(string name)
        {
            return "Public " + name;
        }

        public string Getprivateinformation(string name)
        {
            try { }
            catch (Exception ex)
            {
                //throw new FaultException("Communication channel is faulted wantedly  to show a demo as wshttpbinding(apart from basicHttpbinding as it will not have any session with http connection) secure connection communication will get faulted on any .NET exception at service side ..Then A new instance of the proxy class needs to be created at client side");
                CustomFaultImplementation cfex = new CustomFaultImplementation();
                cfex.Error = ex.Message;
                cfex.Details = "Communication channel is faulted wantedly  to show a demo as wshttpbinding(apart from basicHttpbinding as it will not have any session with http connection) secure connection communication will get faulted on any .NET exception at service side ..Then A new instance of the proxy class needs to be created at client side";
                throw new FaultException<CustomFaultImplementation>(cfex);

            }

                // --------------Part 19  To create a most strongly typed SOAP fault
                //1. Create a class(like CustomFaultImplementation.cs) that represents your SOAP fault. Decorate the class withDataContract attribute and the properties with DataMember attribute.
                //2. In the service data contract, use FaultContractAttribute to specify which operations can throw which SOAP faults.
                //3. In the service implementation create an instance of the strongly typed SOAP fault and throw it using FaultException<T>. 

            //---------------Part 20  Centralized exception handling in WCF by implementing IErrorHandler interface in a custom GlobalErrorHandler.cs class
            //Step 1: Implement IErrorHandler interface. To do this, right click on CalculatorService project and add a class file with name = GlobalErrorHandler.cs
            //    IErrorHandler interface has 2 methods for which we need to provide implementation.
            //    1. ProvideFault() - This method gets called automatically when there is an unhandled exception or a fault. In this method we have the opportunity to write code to convert the unhandled exception into a generic fault that can be returned to the client. ProvideFault() gets called before HandleError() method.
            //    2. HandleError() - This method gets called asynchronously after ProvideFault() method is called and the error message is returned to the client. This means that this method allows us to write code to log the exception without blocking the client call.
            //Step 2: Create a custom Service Behaviour Attribute to let WCF know that we want to use the GlobalErrorHandler class whenever an unhandled exception occurs. To do this, right click on the CalculatorService project and add a class file with name =  GlobalErrorHandlerBehaviourAttribute.cs.
                    //           Notice that the GlobalErrorHandlerBehaviourAttribute class
                    //1. Inherits from Attribute abstract class.
                    //2. Implements IServiceBehavior interface. This interface has 3 methods (Validate(), AddBindingParameters(), ApplyDispatchBehavior()). The implementation for Validate() and AddBindingParameters() method can be left blank. In the ApplyDispatchBehavior() method, we create an instance of the GlobalErrorHandler class and associate the instance with each channelDispatcher. 
                    //3. Has a constructor that contains one Type parameter. We will use this constructor in Step 3.
            //Step 3: Decorate CalculatorService class in CalculatorService.cs file withGlobalErrorHandlerBehaviourAttribute. Notice that this attribute has one constructor that expects a single Type parameter. Pass GlobalErrorHandler class created in Step 1 as the argument
            //For testing the global centralized error handler
            //Comment try-catch blocks in the Divide() method in CalculatorService.cs file

            //========== To return Login used by WCF service for DB connection
            string cs = ConfigurationManager.ConnectionStrings["WCF"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select SUSER_NAME()", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //SqlParameter parameterId = new SqlParameter();
                con.Open();
                //SqlDataReader sqluser = cmd.ExecuteScalar();
                name =(string) cmd.ExecuteScalar();

                
            }


            //============


            return "Private and SQLServer Login name " + name;
        }

        //Service based on Employee details
             
        public WCFEmployeeWOEF GetWCFEmployee(int Id)          
        {                                                  
                                                           
            WCFEmployeeWOEF employee = null; //Initializing it to null as Known type attribute was not working      
            string cs = ConfigurationManager.ConnectionStrings["WCF"].ConnectionString;
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetWCFEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameterId = new SqlParameter();
                parameterId.ParameterName = "@Id";
                parameterId.Value = Id;
                cmd.Parameters.Add(parameterId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //Complex employee type code part
                    if ((EmployeeType)reader["EmployeeType"] ==EmployeeType.FullTimeEmployee)
                    {
                        employee = new FullTimeEmployee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = EmployeeType.FullTimeEmployee,
                            AnnualSalary = Convert.ToInt32(reader["AnnualSalary"])
                        };
                    }
                    else
                    {
                        employee = new PartTimeEmployee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                            Type = EmployeeType.PartTimeEmployee,
                            HourlyPay = Convert.ToInt32(reader["HourlyPay"]),
                            HoursWorked = Convert.ToInt32(reader["HoursWorked"]),
                        };
                    }
                }
            }

            //Commenting the simple employee type code part
            //        employee.Id = Convert.ToInt32(reader["Id"]);
            //        employee.Name = reader["Name"].ToString();
            //        employee.Gender = reader["Gender"].ToString();
            //        employee.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
            //    }
            //}
            return employee;
        }

        public void SaveWCFEmployee(WCFEmployeeWOEF Employee)
        {

            string cs = ConfigurationManager.ConnectionStrings["WCF"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spSaveWCFEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Commenting to leave employee Id to autogenerate at Database side
                //SqlParameter parameterId = new SqlParameter
                //{
                //    ParameterName = "@Id",
                //    Value = Employee.Id
                //};
                //cmd.Parameters.Add(parameterId);

                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = Employee.Name
                };
                cmd.Parameters.Add(parameterName);

                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender",
                    Value = Employee.Gender
                };
                cmd.Parameters.Add(parameterGender);

                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = Employee.DateOfBirth
                };
                cmd.Parameters.Add(parameterDateOfBirth);

                //Adding more attributes for complex Employee type
                SqlParameter parameterEmployeeType = new SqlParameter
                {
                    ParameterName = "@EmployeeType",
                    Value = Employee.Type
                };
                cmd.Parameters.Add(parameterEmployeeType);

                if (Employee.GetType() == typeof(FullTimeEmployee)) // To decide type/class of data getting to save 
                {
                    SqlParameter parameterAnnualSalary = new SqlParameter
                    {
                        ParameterName = "@AnnualSalary",
                        Value = ((FullTimeEmployee)Employee).AnnualSalary
                    };
                    cmd.Parameters.Add(parameterAnnualSalary);
                }
                else
                {
                    SqlParameter parameterHourlyPay = new SqlParameter
                    {
                        ParameterName = "@HourlyPay",
                        Value = ((PartTimeEmployee)Employee).HourlyPay,
                    };
                    cmd.Parameters.Add(parameterHourlyPay);

                    SqlParameter parameterHoursWorked = new SqlParameter
                    {
                        ParameterName = "@HoursWorked",
                        Value = ((PartTimeEmployee)Employee).HoursWorked
                    };
                    cmd.Parameters.Add(parameterHoursWorked);
                }



                con.Open();
                cmd.ExecuteNonQuery();

                //Reference for using SQLDATAADAPTER http://csharp.net-informations.com/dataadapter/insertcommand-sqlserver.htm
                //adapter.InsertCommand = new SqlCommand(sql, connection);
                //adapter.InsertCommand.ExecuteNonQuery();
                //MessageBox.Show("Row inserted !! ");
            }
        }

        public string UpdateWCFEmployee(WCFEmployeeWOEF Employee)
        {
            string cs = ConfigurationManager.ConnectionStrings["WCF"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spUpdateWCFEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                
                SqlParameter parameterName = new SqlParameter
                {
                    ParameterName = "@Name", 
                    Value = Employee.Name
                };
                cmd.Parameters.Add(parameterName);

                SqlParameter parameterGender = new SqlParameter
                {
                    ParameterName = "@Gender", 
                    Value = Employee.Gender
                };
                cmd.Parameters.Add(parameterGender);

                SqlParameter parameterDateOfBirth = new SqlParameter
                {
                    ParameterName = "@DateOfBirth",
                    Value = Employee.DateOfBirth
                };
                cmd.Parameters.Add(parameterDateOfBirth);

                //Adding more attributes for complex Employee type
                SqlParameter parameterEmployeeType = new SqlParameter
                {
                    ParameterName = "@EmployeeType",
                    Value = Employee.Type
                };
                cmd.Parameters.Add(parameterEmployeeType);

                if (Employee.GetType() == typeof(FullTimeEmployee)) // To decide type/class of data getting to save 
                {
                    SqlParameter parameterAnnualSalary = new SqlParameter
                    {
                        ParameterName = "@AnnualSalary",
                        Value = ((FullTimeEmployee)Employee).AnnualSalary
                    };
                    cmd.Parameters.Add(parameterAnnualSalary);
                }
                else
                {
                    SqlParameter parameterHourlyPay = new SqlParameter
                    {
                        ParameterName = "@HourlyPay",
                        Value = ((PartTimeEmployee)Employee).HourlyPay,
                    };
                    cmd.Parameters.Add(parameterHourlyPay);

                    SqlParameter parameterHoursWorked = new SqlParameter
                    {
                        ParameterName = "@HoursWorked",
                        Value = ((PartTimeEmployee)Employee).HoursWorked
                    };
                    cmd.Parameters.Add(parameterHoursWorked);

                
                }

                SqlParameter parameterIdtochange = new SqlParameter
                {
                    ParameterName = "@Id",
                    Value = Employee.Id
                };
                cmd.Parameters.Add(parameterIdtochange);



                con.Open();
                cmd.ExecuteNonQuery();
            }

            string Updatemessage = Employee.Name + " profile has been updated succesfully";

            return Updatemessage;
            //throw new NotImplementedException();
        }

        public FileProp FileDownload()
        {
            FileProp Fobj = new FileProp();
            Fobj.Content = System.IO.File.ReadAllBytes(@"D:\MS DotNet\WCF\testfile.txt");
            Fobj.Name = "File Downloaded From WCF.txt";
            return Fobj;
            //throw new NotImplementedException();
        }

        //WCF Restfull service implementations

        public WCFEmployee GetWCFRESTEmployee(int Id)
        {
            WCFEmployee_CFEXDB_DBContext wcfctxt = new WCFEmployee_CFEXDB_DBContext();

            //WCFEmployee singleemp = from s in wcfctxt.WCFEmployees.AsQueryable()                                    .Where(s => s.Id == Id)
            //                        select new WCFEmployee
            //                        {
            //                            Id = s.Id,
            //                            Name = s.Name,
            //                            Gender = s.Gender,
            //                            DateOfBirth = s.DateOfBirth,
            //                            EmployeeType = s.EmployeeType,
            //                            AnnualSalary = s.AnnualSalary,
            //                            HourlyPay = s.HourlyPay,
            //                            HoursWorked = s.HoursWorked
            //                        }; 
            WCFEmployee singleemp = wcfctxt.WCFEmployees.Find(Id);
            return singleemp;
            
            //throw new NotImplementedException();
        }

        public void SaveWCFERESTmployee(WCFEmployee Employee)
        {
            WCFEmployee_CFEXDB_DBContext wcfctxt = new WCFEmployee_CFEXDB_DBContext();

            wcfctxt.WCFEmployees.Add(Employee);
            wcfctxt.SaveChanges();

            //throw new NotImplementedException();
        }

        public string UpdateWCFRESTEmployee(WCFEmployee Employee)
        {
            WCFEmployee_CFEXDB_DBContext wcfctxt = new WCFEmployee_CFEXDB_DBContext();
            WCFEmployee toupdt = wcfctxt.WCFEmployees.Find(Employee.Id);
            toupdt.Name = Employee.Name;
            toupdt.Gender = Employee.Gender;
            toupdt.DateOfBirth = Employee.DateOfBirth;
            toupdt.EmployeeType = Employee.EmployeeType;
            toupdt.AnnualSalary = Employee.AnnualSalary;
            toupdt.HourlyPay = Employee.HourlyPay;
            toupdt.HoursWorked = Employee.HoursWorked;
            wcfctxt.SaveChanges();

            return toupdt.Name + " details Updated successfully";
            //throw new NotImplementedException();
        }

        public List<WCFEmployee> ALLWCFRESTEmployee()
        {
            WCFEmployee_CFEXDB_DBContext wcfctxt = new WCFEmployee_CFEXDB_DBContext();
            List<WCFEmployee> empset= wcfctxt.WCFEmployees.ToList();
            //IEnumerable<WCFEmployee> empset=from es in wcfctxt.WCFEmployees.AsEnumerable()
            //                                orderby es.Id
            //                                select new WCFEmployee
            //                                {
            //                                    Id = es.Id,
            //                                    Name = es.Name,
            //                                    Gender = es.Gender,
            //                                    DateOfBirth = es.DateOfBirth,
            //                                    EmployeeType = es.EmployeeType,
            //                                    AnnualSalary = es.AnnualSalary,
            //                                    HourlyPay = es.HourlyPay,
            //                                    HoursWorked = es.HoursWorked
            //                                };
            return empset; 
            //throw new NotImplementedException();
        }
    }
}
