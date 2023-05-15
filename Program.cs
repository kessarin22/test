using System;
using System.Collections.Generic;

class city
{
    public string Name (get; set; )  
    public int ID (get; set; ) 
    public int InfectionLevel (get; set; )
    public List<int> ConnectedCities (get; set; )

    public City(string name, int id)
    {
        Name = name;
        ID = id;
        InfectionLevel = 0;
        ConnectedCities = new List<int>();
    }
}

class Program
{
    static void Main()
    {
        Console.Write("จำนวนเมืองที่แสดงในแบบจำลอง: ");
        int numCities = int.Parse(Console.ReadLine());

        List<City> cities = new List<City>();

        for (int i = 0; i < numCities; i++)
        {
            Console.Write("ชื่อเมืองที่ {i}: ");
            string cityName = Console.ReadLine();

            City city = new City(cityName, i);
            cities.Add(city);
        }

        string eventMessage;
        do
        {
            Console.Write("เหตุการณ์ที่เกิดขึ้นระหว่างการระบาดของโรค COVID-19 (Outbreak, Vaccinate, Lock down, Spread, Exit): ");
            eventMessage = Console.ReadLine();

            switch (eventMessage)
            {
                case "Outbreak":
                case "Vaccinate":
                case "Lock down":
                    Console.Write("หมายเลขประจำเมืองที่เกิดเหตุการณ์: ");
                    int cityID = int.Parse(Console.ReadLine());

                    if (cityID < 0 || cityID >= numCities)
                    {
                        Console.WriteLine("Invalid ID");
                        break;
                    }

                    City selectedCity = cities[cityID];
                    switch (eventMessage)
                    {
                        case "Outbreak":
                            if (selectedCity.InfectionLevel <= 1 && selectedCity.InfectionLevel < 3)
                            {
                                selectedCity.InfectionLevel += 2;
                                foreach (int connectedCityID in selectedCity.ConnectedCities)
                                {
                                    City connectedCity = cities[connectedCityID];
                                    if (connectedCity.InfectionLevel < 3)
                                        connectedCity.InfectionLevel += 1;
                                }
                            }
                            break;
                        case "Vaccinate":
                            selectedCity.InfectionLevel = 0;
                            break;
                        case "Lock down":
                            if (selectedCity.InfectionLevel > 0)
                            {
                                selectedCity.InfectionLevel -= 1;
                                foreach (int connectedCityID in selectedCity.ConnectedCities)
                                {
                                    City connectedCity = cities[connectedCityID];
                                    if (connectedCity.InfectionLevel > 0)
                                        connectedCity.InfectionLevel -= 1;
                                }
                            }
                            break;
                    }
                    break;
                case "Spread":
                    bool isSpread = false;
                    foreach (City city in cities)
                    {
                        foreach (int connectedCityID in city.ConnectedCities)
                        {
                            City connectedCity = cities[connectedCityID];
                            if (connectedCity.InfectionLevel > city.InfectionLevel)
                            {
                                city.InfectionLevel += 1;
                                isSpread = true;
                                break;
                            }
                        }
                    }
                    

