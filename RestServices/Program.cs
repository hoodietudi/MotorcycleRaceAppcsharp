using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.model;
using Newtonsoft.Json;

namespace RestServices
{
    internal class Program
    {
        static HttpClient client = new HttpClient();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RunAsync().Wait();
        }


        static async Task RunAsync()
        {
            
            Console.WriteLine("Afisez toate cursele");
            Race[] races = await FindAll("http://localhost:8080/race");
            foreach (Race race in races)
            {
                Console.WriteLine(race.ToString());
            }
            Console.WriteLine();
          
            Console.WriteLine("Caut cursa cu ID-ul 1");
            Race race1 = await FindOne("http://localhost:8080/race/1");
            Console.WriteLine(race1.ToString());
            Console.WriteLine();

            

           Console.WriteLine("Adaug o race");
            Race race2 = new Race();
            race2.RequiredEngineCapacity = EngineCapacity.EngineCapacity125Cmc;
            race2.Name = "cursanoua";
            Race race3 = await Add("http://localhost:8080/race/", race2);
            Console.WriteLine(race3.ToString());
            Console.WriteLine();
            Console.WriteLine("Afisez toate cursele DUPA ADAUGARE");
            
            foreach (Race proba in await FindAll("http://localhost:8080/race/"))
            {
                Console.WriteLine(proba.ToString());
            }
            Console.WriteLine();

                 Console.WriteLine("O actualizez");
                 Race race4 = new Race();
                 race4.ID = race3.ID; 
                 race4.RequiredEngineCapacity = EngineCapacity.EngineCapacity500Cmc;
                 race4.Name = "cursanoua";
                 Race race5 = await Update("http://localhost:8080/race/"+race4.ID, race4);
                 Console.WriteLine("Am actualizat! Cursa este: " + race5.ToString());
                 Console.WriteLine();
                 
                 Console.WriteLine("Afisez toate cursele DUPA actualizare");
            
                 foreach (Race race in await FindAll("http://localhost:8080/race/"))
                 {
                     Console.WriteLine(race.ToString());
                 }
                 Console.WriteLine();

         Console.WriteLine("O sterg");
            await client.DeleteAsync("http://localhost:8080/race/"+race4.ID);
            Console.WriteLine("Am sters!");
            Console.WriteLine();

            Console.WriteLine("Cursele sunt");
            foreach (Race proba in await FindAll("http://localhost:8080/race/"))
            {
                Console.WriteLine(proba.ToString());
            }

            Console.Read();
            
        }

        static async Task<String> GetTextAsync(string path)
        {
            String product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsStringAsync();
            }
            return product;
        }


        static async Task<Race> GetProbaAsync(string path)
        {
            Race product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Race>();
            }
            return product;
        }
        
        static async Task<Race> FindOne(string path)
        {
            Race race = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                race = await response.Content.ReadAsAsync<Race>();
            }
            return race;
        }

        static async Task<Race[]> FindAll(string path)
        {
            Race[] races = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                races = await response.Content.ReadAsAsync<Race[]>();
            }
            return races;
        }

        static async Task<Race> Add(string path, Race proba)
        {
            Race result = null;
            HttpResponseMessage response = await client.PostAsJsonAsync<Race>(path, proba);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Race>();
            }
            return result;
        }

        static async Task<Race> Update(string path, Race race)
        {
            Race result = null;
            HttpResponseMessage response = await client.PutAsJsonAsync<Race>(path, race);
            if (response.IsSuccessStatusCode)
            {
                result = await FindOne("http://localhost:8080/race/"+race.ID);
            }
            return result;
        }

    }

    public class Race
    {

        [JsonProperty("requiredEngineCapacity")]
        public EngineCapacity RequiredEngineCapacity{ get; set; }
        
        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("id")]
        public int ID { get; set; }
        public override string ToString()
        {
            return string.Format("[Race: RequiredEngineCapacity={0}, Name={1}, ID={2}]", RequiredEngineCapacity,Name, ID);
        }
    }
}