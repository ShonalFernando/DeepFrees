using DeepFrees.PayrollServices.MicroService;
using Microsoft.AspNetCore.Mvc;
using DeepFrees.PayrollServices.Model;
using Hangfire;
using DeepFrees.EmployeeService.Model;
using ZstdSharp.Unsafe;

namespace DeepFrees.PayrollServices.Controllers
{
    [ApiController]
    [Route("/payroll")]
    public class PayrollController : Controller
    {
        private readonly DataService _DataService;
        private readonly PaySheetGenerator _PaySheetGenerator;

        public PayrollController(DataService dataService, PaySheetGenerator paySheetGenerator)
        {
            _DataService = dataService;
            _PaySheetGenerator = paySheetGenerator;
        }

        //Switch to Ignite Scheduled Sallary Calculation Operations
        [HttpGet("{NIC}")]
        public async Task<IActionResult> GetSallary([FromRoute] string NIC)
        {
            EmployeePR? Employee = await _DataService.GetEmployee(NIC);

            if(Employee != null)
            {
                return Ok(Employee.SallaryData);
            }

            return NotFound();
        }

        //Switch to Ignite Scheduled Sallary Calculation Operations
        [HttpGet("DFPRIgnite")]
        public async Task<IActionResult> ToggleDFPRJobTasks([FromQuery] bool _isRunning)
        {
            if(_isRunning)
            {
                await Console.Out.WriteLineAsync("Job Schedule started! Every 28th Sallary will be calculated");
                await Console.Out.WriteLineAsync("===========================================================");
                await Console.Out.WriteLineAsync("Initializing...");
                await Console.Out.WriteLineAsync("===========================================================");

                RecurringJob.AddOrUpdate("SalCalJob", () => CalculateSallary(), Cron.Minutely /*"0 0 0 28 * ?"*/);
            }
            else
            {
                await Console.Out.WriteLineAsync("Sallary Calculation Job Stoped");
                await Console.Out.WriteLineAsync("===========================================================");

                RecurringJob.RemoveIfExists("SalCalJob");
            }
            return Ok();
        }

        //Switch to manually Scheduled Sallary Calculation Operations
        [HttpGet("CalculateSallary")]
        public async Task<IActionResult> CalculateSallary()
        {
            var Employees = await _DataService.GetEmployee();
            foreach(var Employee in Employees)
            {

                if(Employee.SallaryData.MonthlySallarySheets != null)
                {
                    await Console.Out.WriteLineAsync("Employee : " + Employee.NIC + " : Calculating");
                    var UpdatedSallData = _PaySheetGenerator.CalculatePaySheets(Employee.SallaryData);
                    Employee.SallaryData = UpdatedSallData;
                    await _DataService.UpdateEmployee(Employee.NIC, Employee);         
                    
                    foreach(var Sallaries in Employee.SallaryData.MonthlySallarySheets)
                    {
                        if(Sallaries.Year == DateTime.Now.Year && Sallaries.Month == DateTime.Now.Month)
                        {
                            await Console.Out.WriteLineAsync("Employee Sallary: " + Sallaries.NetSallary);
                        }
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync("No Sallary Sheet available to Calculate! For Employee: " + Employee.NIC);
                }
            }
            return Ok();
        }

        //This method is used to MANUALLY (or triggered by Auto Jobs)
        //Create a Sallary for a certain month with greater than this month
        [HttpPost("AddSheet/{NIC}")]
        public async Task<IActionResult> Post([FromRoute] string NIC,[FromQuery] int SMonth, [FromQuery] int SYear)
        {
            EmployeePR? Employee = await _DataService.GetEmployee(NIC);

            if (Employee == null)
            {
                return NotFound();
            }
            else
            {
                if (Employee.SallaryData.MonthlySallarySheets == null)
                {
                    return NotFound();
                }
                else
                {
                    bool isAvailable = Employee.SallaryData.MonthlySallarySheets.Any(s => s.Month == SMonth && s.Year == SYear);
                
                    if(!isAvailable)
                    {
                        MonthlySallarySheet MonthlySallarySheet = new();
                        MonthlySallarySheet.Year = SMonth;
                        MonthlySallarySheet.Month = SMonth;
                        MonthlySallarySheet.Increments = new List<Tuple<double, string>>();
                        Employee.SallaryData.MonthlySallarySheets.Add(MonthlySallarySheet);

                        return Ok();
                    }
                    else
                    {
                        {
                            return BadRequest();
                        }
                    }
                
                }
            }
        }

        //This method is used to Update Sallary Data as Whole : Specaily for the basic information
        [HttpPut("Update/{NIC}")]
        public async Task<IActionResult> Update([FromRoute] string NIC, [FromBody] SallaryData SallaryData)
        {
            EmployeePR? Employee = await _DataService.GetEmployee(NIC);

            if (Employee == null)
            {
                return NotFound();
            }
            else
            {
                if(SallaryData == null)
                {
                    return BadRequest();
                }
                else
                {
                    SallaryData.MonthlySallarySheets = Employee.SallaryData.MonthlySallarySheets; //This prevents overr
                    Employee.SallaryData = SallaryData;
                    await _DataService.UpdateEmployee(Employee.NIC, Employee);
                    return Ok();
                }
            }
        }

        //This method is used to Update This Month Data like OT , etc...
        [HttpPut("MonthlyAlter/{NIC}")]
        public async Task<IActionResult> UpdateMonth([FromRoute] string NIC,[FromBody] MonthlySallarySheet MonthlyUpdatedSallarySheet)
        {
            EmployeePR? Employee = await _DataService.GetEmployee(NIC);

            if (Employee == null)
            {
                return NotFound();
            }
            else
            {
                if (MonthlyUpdatedSallarySheet == null )
                {
                    return BadRequest();
                }
                else if (Employee.SallaryData.MonthlySallarySheets == null)
                {
                    return NotFound();
                }
                else
                {
                    foreach(var Sheet in Employee.SallaryData.MonthlySallarySheets)
                    {
                        if(Sheet.Month == DateTime.Now.Month && Sheet.Year == DateTime.Now.Year)
                        {
                            Sheet.Deductions = MonthlyUpdatedSallarySheet.Deductions;
                            Sheet.OTHours = MonthlyUpdatedSallarySheet.OTHours;
                            Sheet.NonMedicalLeaves = MonthlyUpdatedSallarySheet.NonMedicalLeaves;
                            Sheet.MedicalLeaves = MonthlyUpdatedSallarySheet.MedicalLeaves;
                            Sheet.Tax = MonthlyUpdatedSallarySheet.Tax;
                            Sheet.EmployeeFund = MonthlyUpdatedSallarySheet.EmployeeFund;
                            Sheet.Increments = MonthlyUpdatedSallarySheet.Increments;

                            return Ok();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return NotFound();
                }
            }
        }

        //Delete a PaySheet : This is Dangerous
        [HttpDelete("Delete/{NIC}")]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }
    }
}
