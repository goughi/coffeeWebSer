using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using CoffeeWebService.Models;

namespace CoffeeWebService.Controllers
{
    [RoutePrefix("CoffeeStore")]
    public class CoffeeStoreController : ApiController
    {
        /*
            Get /CoffeeStore/all                gets all the coffee stores          GetAllCoffeeStores
            Get /CoffeeStore/Name/Costa         get all stores named Costa          GetStoreByName
            Get /CoffeeStore/Dublin/isOpen/hasWifi/highest rating     get all stores in Dublin / isOpen / hasWifi / sort by rating      GetDublinStoresOpenHasWifiInAreaSort
            Get /CoffeeStore/Dublin/isOpen/hasWifi/lowest rating     get all stores in Dublin / isOpen / hasWifi / sort by low rating      GetDublinStoresOpenHasWifiInAreaSortLow
            Get /CoffeeStore/Limerick/isOpen/hasWifi/highest rating   get all stores in Limerick / isOpen / hasWifi / sort by rating    GetLimerickStoresOpenHasWifiInAreaSort
            Get /CoffeeStore/Cork/isOpen/hasWifi/highest rating       get all stores in Cork/ isOpen / hasWifi / sort by rating         GetCorkStoresOpenHasWifiInAreaSort
            Get /CoffeeStore/Dublin/isOpen/hasWifi          get all stores in Dublin / isOpen / hasWifi / sort by rating                GetDublinStoresOpenHasWifiInArea
            Get /CoffeeStore/eircode/isOpen/D02             get all stores open with eircode D02                        GetAllStoresOpenInArea
            Get /CoffeeStore/eircode/V94                    get all stores with eircode V94                             GetAllStoreInArea 
            Get /CoffeeStore/eircode/hasWifi/isOpen/D01     get all stores open with wifi eircode D01                   GetStoresWithWifiIsOpenInArea
            Get /CoffeeStore/Review/all                     get all reviews                                             GetAllReviews
            Get /CoffeeStore/Review/latest/5                get the 5 latest reviews                                    GetLatestReviews
            Post /api/CoffeeStore                           Post review                                                 PostAddReview
        */
        private DrinkContext db = new DrinkContext();

        [Route("all")]
        //Get CoffeeStore all
        public IHttpActionResult GetAllCoffeeStores()
        {
            return Ok(db.CoffeeStores.ToList());
        }

        [Route("Name/{name:alpha}")]
        //Get coffeeStores by name
        public IHttpActionResult GetStoreByName(String name)
        {
            if (ModelState.IsValid)
            {
                var results = db.CoffeeStores.Where(s => s.StoreName.ToUpper() == name.ToUpper());
                //Are there any stores with this name?
                if (results.Count() > 0)
                {

                    return Ok(results.ToList());
                }
                return NotFound();
            }
            return BadRequest();
        }


        [Route("Dublin/isOpen/hasWifi/highestRating")]
        //get all stores in Dublin / isOpen / hasWifi / sort by rating
        public IHttpActionResult GetDublinStoresOpenHasWifiInAreaSort()
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.City == City.Dublin
                && s.IsOpen == true
                && s.hasWifi == true).OrderByDescending(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("Dublin/isOpen/hasWifi/lowestRating")]
        //get all stores in Dublin / isOpen / hasWifi / sort by rating
        public IHttpActionResult GetDublinStoresOpenHasWifiInAreaSortLow()
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.City == City.Dublin
                && s.IsOpen == true
                && s.hasWifi == true).OrderBy(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("Limerick/isOpen/hasWifi/highestRating")]
        //get all stores in Limerick / isOpen / hasWifi / sort by rating
        public IHttpActionResult GetLimerickStoresOpenHasWifiInArea()
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.City == City.Limerick
                && s.IsOpen == true
                && s.hasWifi == true).OrderByDescending(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("Cork/isOpen/hasWifi/highestRating")]
        //get all stores in Cork / isOpen / hasWifi / sort by rating
        public IHttpActionResult GetCorkStoresOpenHasWifiInArea()
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.City == City.Cork
                && s.IsOpen == true
                && s.hasWifi == true).OrderByDescending(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }
        [Route("Dublin/isOpen/hasWifi")]
        //get all stores in Dublin / isOpen / hasWifi / sort by rating
        public IHttpActionResult GetDublinStoresOpenHasWifiInArea()
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.City == City.Dublin
                && s.IsOpen == true
                && s.hasWifi == true);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }


        [Route("eircode/isOpen/{eircode}")]
        //get all stores in specified area / isOpen
        public IHttpActionResult GetAllStoresOpenInArea(string eircode)
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.Eircode.Substring(0, 3).ToUpper() == eircode.ToUpper()
                && s.IsOpen == true).OrderByDescending(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("eircode/{eircode}")]
        //get all stores in specified area 
        public IHttpActionResult GetAllStoreInArea(string eircode)
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.Eircode.Substring(0, 3).ToUpper() == eircode.ToUpper());
                
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("eircode/hasWifi/{eircode}")]
        //get all stores in specified area / isOpen
        public IHttpActionResult GetAllStoresWithWifiInArea(string eircode)
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.Eircode.Substring(0, 3).ToUpper() == eircode.ToUpper()
                && s.hasWifi == true).OrderByDescending(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("eircode/hasWifi/isOpen/{eircode}")]
        //get all stores in specified area /hasWifi/ IsOpen
        public IHttpActionResult GetStoresWithWifiIsOpenInArea(string eircode)
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.Eircode.Substring(0, 3).ToUpper() == eircode.ToUpper()
                && s.IsOpen == true
                && s.hasWifi == true);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("Review/all")]
        //Get Review all
        public IHttpActionResult GetAllReviews()
        {
            return Ok(db.Reviews.ToList());
        }


        [Route("Review/Latest/{num}")]
        //Get Review the latest reviews
        public IHttpActionResult GetLatestReviews(int num)
        {
            var results = db.Reviews.OrderByDescending(r => r.ReviewDate).Take(num);

            if (results.Count() > 0)
            {
                return Ok(results.ToList());
            }
            return NotFound();
        }

        //add new review
        [HttpPost]
        public IHttpActionResult PostAddReview(Review review)
        {
            if(ModelState.IsValid)
            {
                var record = db.Reviews.FirstOrDefault(x => x.ReviewID == review.ReviewID);
                if(record == null)
                {
                    db.Reviews.Add(review);
                    db.SaveChanges();
                    String uri = Url.Link("DefaultApi", new { ReviewID = review.ReviewID });
                    return Created(uri, review);
                }
                else { return NotFound(); }

            }
            else { return BadRequest(); }
        }
    
    }
}