using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyFirstApi.Models;
using System.Data.SqlClient;

namespace MyFirstApi.Controllers
{
    public class MembersController : Controller
    {
        private readonly ILogger<MembersController> _logger;

        public MembersController(ILogger<MembersController> logger)
        {
            _logger = logger;
        }
        [HttpGet("api/members")]
        public object GetMembers ()
            {
                //return new {name="Justin"};
                //declare a variable of sql connection type
                var sqlCon = new SqlConnection("Server=rax-sql02;Database=Franchise;Trusted_Connection=True;");
                //open connection
                sqlCon.Open();
                //create a variable of sqlcommand using the sql connection variable
                var sqlcmd = sqlCon.CreateCommand();
                //create a sql command 
                sqlcmd.CommandText = "Select top(10) memberid from dbo.members";
                //create a variable of sql execution reader to read data from db 
                var rdr = sqlcmd.ExecuteReader();
                //declare a list to collect the data
                var memberid = new List<long>();
                while (rdr.Read()){
                    memberid.Add(rdr.GetInt64(0));
                }



                return memberid;
            }
    }
}
