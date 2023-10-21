using DeepFrees.EmployeeService.Model;

namespace DeepFrees.PayrollServices.MicroService
{
    public class PaySheetGenerator
    {
        //Month start at 1st and ends between 28-31, but
        //New Paysheet will be created at 28th every month for the next month
        public SallaryData CreateNewPaySheet(SallaryData SallaryData)
        {
            if (SallaryData.MonthlySallarySheets != null)
            {
                foreach (var PaySheet in SallaryData.MonthlySallarySheets)
                {
                    if (PaySheet.Year == DateTime.Now.Year && PaySheet.Month == (DateTime.Now.Month + 1))
                    {
                        return SallaryData;
                    }
                    else
                    {
                        MonthlySallarySheet MonthlySallarySheet = new();
                        MonthlySallarySheet.Month = (DateTime.Now.Month + 1);
                        MonthlySallarySheet.Year = (DateTime.Now.Year);
                        SallaryData.MonthlySallarySheets.Add(MonthlySallarySheet);
                        return SallaryData;
                    }
                }
                return SallaryData;
            }
            else
            {
                List<MonthlySallarySheet> monthlySallarySheets = new();
                SallaryData.MonthlySallarySheets = monthlySallarySheets;

                MonthlySallarySheet MonthlySallarySheet = new();
                MonthlySallarySheet.Month = (DateTime.Now.Month + 1);
                MonthlySallarySheet.Year = (DateTime.Now.Year);

                SallaryData.MonthlySallarySheets.Add(MonthlySallarySheet);
                return SallaryData;
            }
        }

        //Sallary Will be Calculated for the Current Month at 28th
        public SallaryData CalculatePaySheets(SallaryData SallaryData)
        {
            if (SallaryData.MonthlySallarySheets != null)
            {
                foreach(var PaySheet in SallaryData.MonthlySallarySheets)
                {
                    if (PaySheet.Year == DateTime.Now.Year && PaySheet.Month == DateTime.Now.Month)
                    {
                        PaySheet.NetSallary = 0; //INIT

                        PaySheet.NetSallary += SallaryData.BaseSallary; //BaseOp
                        PaySheet.NetSallary -= SallaryData.MonthlyCutOff;//BaseOp
                        PaySheet.NetSallary += SallaryData.FineTune;//BaseOp

                        var _diff = SallaryData.CumulativeLeaves - SallaryData.AllocatedYearlyPaidLeaves;

                        if (_diff>0) //Has Unpaid Leaves
                        {
                            if (_diff < PaySheet.NonMedicalLeaves) //Check if Mid Month Cut-Off
                            {
                                PaySheet.NetSallary -= SallaryData.NoPayRatePerDay * (PaySheet.NonMedicalLeaves - _diff);
                            }
                            else
                            {
                                PaySheet.NetSallary -= SallaryData.NoPayRatePerDay * PaySheet.NonMedicalLeaves;
                            }
                        }

                        var _mdiff = SallaryData.CumulativeMedicalLeaves - SallaryData.AllocatedYearlyPaidMedicalLeaves;

                        if (_mdiff>0) //Has Unpaid Medical Leaves
                        {
                            if (_mdiff < PaySheet.MedicalLeaves) //Check if Mid Month Cut-Off
                            {
                                PaySheet.NetSallary -= SallaryData.NoPayRatePerDay * (PaySheet.MedicalLeaves - _mdiff);
                            }
                            else
                            {
                                PaySheet.NetSallary -= SallaryData.NoPayRatePerDay * PaySheet.MedicalLeaves;
                            }
                        }

                        PaySheet.NetSallary += SallaryData.OTRatePerHour * PaySheet.OTHours;//BaseOp

                        PaySheet.NetSallary -= (PaySheet.Deductions + PaySheet.EmployeeFund + PaySheet.Tax); //SheetOp

                        if (PaySheet.Increments != null)
                        {
                            foreach (var Incs in PaySheet.Increments)
                            {
                                PaySheet.NetSallary -= Incs.Item1;
                            } 
                        }
                    }
                }
                return SallaryData;
            }
            else
            {
                return SallaryData;
            }
        }
    }
}
