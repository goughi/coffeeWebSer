using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoffeeWebService.Models;
using System.Collections.Generic;

namespace CoffeeLocatorTests
{
    
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DrinkContext db = new DrinkContext();
            CoffeeStore charleys = new CoffeeStore() { Eircode = "T12GH46", City = City.Dublin, OpeningTime = OpeningHour.AM0700, ClosingTime = ClosingHour.PM1800, Location = "dublin fair city", StoreName = "charleys", hasWifi = true };
            List <Review> reviews = new List<Review>();
            reviews.Add(new Review() { CustomerName = "Stevo", CustomerEmail = "stevo@yahoo.com", Eircode = charleys.Eircode, Comment = "It was great", Rating = 4 });
            reviews.Add(new Review() { CustomerName = "john", CustomerEmail = "john@yahoo.com", Eircode = charleys.Eircode, Comment = "It was great", Rating = 3 });
            reviews.Add(new Review() { CustomerName = "pete", CustomerEmail = "pete@yahoo.com", Eircode = charleys.Eircode, Comment = "It wasn't great", Rating = 2 });

            Assert.AreEqual(charleys.StoreRating, 3);

        }
    }
}
