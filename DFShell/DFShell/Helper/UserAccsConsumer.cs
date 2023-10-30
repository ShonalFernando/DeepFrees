using DeepFreesAccountsServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DFShell.Helper
{
    class UserAccsConsumer
    {
        public async Task<List<UserAccount>> GetAccs(string Username)
        {
            HttpClient httpClient = new HttpClient();

            string apiUrl = "https://localhost:7111/api/Account/" + Username;
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    try
                    {
                        List<UserAccount> Employees = JsonSerializer.Deserialize<List<UserAccount>>(content);
                        if (Employees != null)
                        {
                            return Employees;
                        }
                        else
                        {
                            return new List<UserAccount>();
                        }
                    }
                    catch (Exception)
                    {

                        return new List<UserAccount>();
                    }
                }
                else
                {
                    return new List<UserAccount>();
                }
            }
            else
            {
                return new List<UserAccount>();
            }
        }
    }
}
