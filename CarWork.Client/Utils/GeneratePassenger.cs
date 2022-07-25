using CarWork.Client.Models;
using System;
using System.Collections.Generic;

namespace CarWork.Client.Utils
{
    public static class GeneratePassenger
    {
        public static Passenger Generate()
        {
            return new Passenger()
            {
                Name = NameGenerate(),
                Location = LocationGenerate()
            };
        }

        private static string NameGenerate()
        {
            Random random = new Random();
            var names = new List<string>() { "Tom", "Bob", "Sam", "Biba" };

            var nameIndex = random.Next(0, names.Count - 1);
            return names[nameIndex];
        }

        private static int LocationGenerate()
        {
            var random = new Random();
            var loc = random.Next(0, Map.GetInstance().SlotCount); 

            return loc;

        } 
        
    }
}
