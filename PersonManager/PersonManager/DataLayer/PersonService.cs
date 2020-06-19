using Newtonsoft.Json;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.DataLayer
{
    public class PersonService : IPersonService
    {
        HttpClient _httpClient;

        public PersonService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IList<Person>> GetPeople()
        {
            var people = new List<Person>();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{App.BASE_URL}{App.API_URL}");
            try
            {
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    people = JsonConvert.DeserializeObject<List<Person>>(content);
                    Debug.WriteLine(@"				People successfully adquired.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"				ERROR {ex.Message}");
            }

            return people;
        }

        public async Task<bool> AddPerson(Person person)
        {
            var result = false;

            try
            {
                _httpClient.BaseAddress = new Uri($"{App.BASE_URL}");
                var json = JsonConvert.SerializeObject(person);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var remote = await _httpClient.PostAsync($"{App.API_URL}", content);
                if (remote.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }

            return result;
        }

        public  async Task<bool> DeletePerson(int personId)
        {
            var result = false;

            try
            {
                _httpClient.BaseAddress = new Uri($"{App.BASE_URL}");
                var remote = await _httpClient.DeleteAsync(App.API_URL+"/"+personId);
                if (remote.IsSuccessStatusCode)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"ERROR {ex.Message}");
            }

            return result;
        }
        
        public async Task<Person> GetOldestPerson()
        {
            var oldest = new Person();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{App.BASE_URL}{App.API_URL}/oldest");
            try
            {
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    oldest = JsonConvert.DeserializeObject<Person>(content);
                    Debug.WriteLine(@"				Oldest successfully adquired.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"				ERROR {ex.Message}");
            }

            return oldest;
        }
    }
}
