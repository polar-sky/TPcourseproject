using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University.Models
{
    public enum SortState
    {
        NameAsc,    // по имени по возрастанию
        NameDesc,   // по имени по убыванию
        GroupAsc,  // по компании по возрастанию
        GroupDesc, // по компании по убыванию
        LectAsc,
        LectDesc,
        LabAsc,
        LabDesc,
        CityAsc,
        CityDesc,
        CompanyAsc,
        CompanyDesc,
        DegreeAsc,
        DegreeDesc,
        ArchAsc,
        ArchDesc
    }
}