using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoffeeWebService.Models;

namespace CoffeeWebService.Controllers
{
    [RoutePrefix("CoffeeStore")]
    public class CoffeeStoreController : ApiController
    {
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

        [Route("eircode/{eircode}")]
        //get all stores in specified area with highest rating first
        public IHttpActionResult GetAllStoresInArea(string eircode)
        {
            var stores = db.CoffeeStores.ToList().Where(s => s.Eircode.Substring(0, 3).ToUpper() == eircode.ToUpper()).OrderByDescending(s => s.StoreRating);
            if (stores.Count() > 0)
            {
                return Ok(stores);
            }
            else { return NotFound(); }
        }

        [Route("eircode/hasWifi/{eircode}")]
        //get all stores in specified area that is open and has wifi
        public IHttpActionResult GetStoresWithWifiInArea(string eircode)
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