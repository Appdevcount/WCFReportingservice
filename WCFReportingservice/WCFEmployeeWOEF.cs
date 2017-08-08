using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

//With respect to WCF, Serialization is the process of converting an object into an XML representation. The reverse process, that is reconstructing the same object from the XML is called as Deserialization. 
//By default, WCF uses DataContractSerializer. 
//For a complex type like Customer, Employee, Student to be serialized, the complex type can either be decorated with 
//1. SerializableAttribute or
//2. DataContractAttribute
//With .NET 3.5 SP1 and above, we don't have to explicitly use DataContract or DataMember attributes. The Data Contract Serializer will serialize all public properties of your complex type in an alphabetical order. By default private field and properties are not serialized.
//If we decorate a complex type, with [Serializable] attribute the DataContractSerializerserializes all fields. With [Serializable] attribute we don't have explicit control on what fields to include and exclude in serialized data.
//If we decorate a complex type with [Datacontract] attribute, the DataContractSerializerserializes the fields marked with the [DataMember] attribute. The fields that are not marked with [DataMember] attribute are excluded from serialization. The [DataMember]attribute can be applied either on the private fields or public properties.

namespace WCFReportingservice
{
    //To utilize Inheritance concept
    //WCF service generally accepts and returns the base type. If you expect the service to accept and return inherited types, then use KnownType attribute.

    //  <!-- can also specify known types in the configuration file. This is equivalent to applyingKnownType attribute on the base type, in the sense that it is applicable globally-->
    //  <!--<system.runtime.serialization>
    //  <dataContractSerializer>
    //    <declaredTypes>
    //      <add type="WCFReportingservice.WCFEmployee, WCFReportingservice, Version=1.0.0.0, &#xD;&#xA;            Culture=Neutral, PublicKeyToken=null"
    //                                                 >
    //        <knownType type="WCFReportingservice.FulltimeEmployee, WCFReportingservice, &#xD;&#xA;                    Version=1.0.0.0, Culture=Neutral, PublicKeyToken=null"
    //                                                                          />
    //        <knownType type="WCFReportingservice.ParttimeEmployee, WCFReportingservice, &#xD;&#xA;                    Version=1.0.0.0, Culture=Neutral, PublicKeyToken=null"
    //                                                                          />
    //      </add>
    //    </declaredTypes>
    //  </dataContractSerializer>
    //</system.runtime.serialization>-->

    [KnownType(typeof(FullTimeEmployee))]
    [KnownType(typeof(PartTimeEmployee))]

    [DataContract(Namespace = "http://wcfreportingservice.com/WCFEmployee")]
    public class WCFEmployeeWOEF  //WOEF - WITHOUT ENTITY FRAMEWORK
    {

        private int _id;
        private string _name;
        private string _gender;
        private DateTime _dateOfBirth;

        [DataMember(Order = 1)]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember(Order = 2)]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [DataMember(Order = 3)]
        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        [DataMember(Order = 4)]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        //Added to seggregate type of Employee
        [DataMember(Order = 5)]
        public EmployeeType Type { get; set; }
    }

    public enum EmployeeType
    {
        FullTimeEmployee = 1,
        PartTimeEmployee = 2
    }

}





